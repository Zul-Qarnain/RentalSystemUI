using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace RentalSystemUI.Services
{
    /// <summary>
    /// Service class for Property-related database operations.
    /// </summary>
    public class PropertyService
    {
        private readonly string _connectionString;
        private static List<PropertyModel>? _cachedProperties = null;

        public PropertyService()
        {
            DotNetEnv.Env.Load();
            _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                                ?? "Server=.\\SQLEXPRESS;Database=HomeRentalDB;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        /// <summary>
        /// Gets all properties owned by a landlord.
        /// </summary>
        public PropertyModel? GetPropertyById(int propertyId)
        {
            if (DatabaseState.ConnectionFailed) return null;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = @"SELECT p.PropertyID, p.Title, p.Description, p.Address, p.City, p.RentAmount, p.Status, p.CreatedAt,
                                     (SELECT COUNT(*) FROM REVIEWS r WHERE r.PropertyID = p.PropertyID) AS ReviewCount,
                                     (SELECT TOP 1 ImagePath FROM PROPERTY_IMAGES pi WHERE pi.PropertyID = p.PropertyID) AS FirstImage,
                                     p.AvailabilityStatus, p.Rooms, p.Kitchen, p.WashRoom, p.IsPetAllowed, p.IsAC
                                     FROM PROPERTIES p WHERE p.PropertyID = @PropertyID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@PropertyID", propertyId);
                    cmd.CommandTimeout = 2;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PropertyModel
                            {
                                PropertyID = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Address = reader.GetString(3),
                                City = reader.GetString(4),
                                RentAmount = reader.GetDecimal(5),
                                Status = reader.GetString(6),
                                CreatedAt = reader.GetDateTime(7),
                                ReviewCount = reader.GetInt32(8),
                                FirstImagePath = reader.IsDBNull(9) ? "" : reader.GetString(9),
                                AvailabilityStatus = !reader.IsDBNull(10) && reader.GetBoolean(10),
                                Rooms = !reader.IsDBNull(11) ? reader.GetInt32(11) : 0,
                                Kitchen = !reader.IsDBNull(12) ? reader.GetInt32(12) : 0,
                                WashRoom = !reader.IsDBNull(13) ? reader.GetInt32(13) : 0,
                                IsPetAllowed = !reader.IsDBNull(14) && reader.GetBoolean(14),
                                IsAC = !reader.IsDBNull(15) && reader.GetBoolean(15)
                            };
                        }
                    }
                }
                return null;
            }
            catch
            {
                DatabaseState.MarkFailed();
                return null;
            }
        }

        public List<PropertyModel> GetPropertiesByLandlord(int landlordId)
        {
            _cachedProperties = null;
            if (DatabaseState.ConnectionFailed) return new List<PropertyModel>();

            try
            {
                List<PropertyModel> properties = new List<PropertyModel>();

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = @"SELECT p.PropertyID, p.Title, p.Description, p.Address, p.City, p.RentAmount, p.Status, p.CreatedAt,
                                     (SELECT COUNT(*) FROM REVIEWS r WHERE r.PropertyID = p.PropertyID) AS ReviewCount,
                                     (SELECT TOP 1 ImagePath FROM PROPERTY_IMAGES pi WHERE pi.PropertyID = p.PropertyID) AS FirstImage,
                                     p.AvailabilityStatus, p.Rooms, p.Kitchen, p.WashRoom, p.IsPetAllowed, p.IsAC
                                     FROM PROPERTIES p WHERE p.LandlordID = @LandlordID ORDER BY p.CreatedAt DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@LandlordID", landlordId);
                    cmd.CommandTimeout = 2;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            properties.Add(new PropertyModel
                            {
                                PropertyID = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Address = reader.GetString(3),
                                City = reader.GetString(4),
                                RentAmount = reader.GetDecimal(5),
                                Status = reader.GetString(6),
                                CreatedAt = reader.GetDateTime(7),
                                ReviewCount = reader.GetInt32(8),
                                FirstImagePath = reader.IsDBNull(9) ? "" : reader.GetString(9),
                                AvailabilityStatus = !reader.IsDBNull(10) && reader.GetBoolean(10),
                                Rooms = !reader.IsDBNull(11) ? reader.GetInt32(11) : 0,
                                Kitchen = !reader.IsDBNull(12) ? reader.GetInt32(12) : 0,
                                WashRoom = !reader.IsDBNull(13) ? reader.GetInt32(13) : 0,
                                IsPetAllowed = !reader.IsDBNull(14) && reader.GetBoolean(14),
                                IsAC = !reader.IsDBNull(15) && reader.GetBoolean(15)
                            });
                        }
                    }
                }

                _cachedProperties = properties;
                return properties;
            }
            catch
            {
                DatabaseState.MarkFailed();
                return new List<PropertyModel>();
            }
        }

        public List<string> GetPropertyImages(int propertyId)
        {
            if (DatabaseState.ConnectionFailed) return new List<string>();
            try
            {
                List<string> images = new List<string>();
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT ImagePath FROM PROPERTY_IMAGES WHERE PropertyID = @PropertyID", conn);
                    cmd.Parameters.AddWithValue("@PropertyID", propertyId);
                    cmd.CommandTimeout = 2;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                        while (reader.Read()) images.Add(reader.GetString(0));
                }
                return images;
            }
            catch
            {
                DatabaseState.MarkFailed();
                return new List<string>();
            }
        }

        public List<ReviewModel> GetReviewsForProperty(int propertyId)
        {
            if (DatabaseState.ConnectionFailed) return new List<ReviewModel>();
            try
            {
                List<ReviewModel> reviews = new List<ReviewModel>();
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"SELECT r.ReviewID, r.Rating, r.Comment, r.CreatedAt, u.FullName FROM REVIEWS r INNER JOIN USERS u ON r.TenantID = u.UserID WHERE r.PropertyID = @PropertyID", conn);
                    cmd.Parameters.AddWithValue("@PropertyID", propertyId);
                    cmd.CommandTimeout = 2;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                        while (reader.Read())
                            reviews.Add(new ReviewModel { ReviewID = reader.GetInt32(0), Rating = reader.GetInt32(1), Comment = reader.IsDBNull(2) ? "" : reader.GetString(2), CreatedAt = reader.GetDateTime(3), TenantName = reader.GetString(4) });
                }
                return reviews;
            }
            catch
            {
                DatabaseState.MarkFailed();
                return new List<ReviewModel>();
            }
        }

        public int AddProperty(int landlordId, string title, string description, string address, string city, decimal rentAmount, string status,
                               int rooms, int kitchen, int washroom, bool isPet, bool isAc, bool availability, List<string>? imagePaths = null)
        {
            try
            {
                var currentUser = AppSession.CurrentUser;
                if (currentUser == null)
                    throw new InvalidOperationException("No active user session.");

                if (!string.Equals(currentUser.UserType, "Landlord", StringComparison.OrdinalIgnoreCase))
                    throw new UnauthorizedAccessException("Only homeowners can add properties.");

                landlordId = currentUser.UserID;

                if (string.IsNullOrWhiteSpace(status)) status = "Available";
                if (!availability) availability = true;

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string sql = @"INSERT INTO PROPERTIES (LandlordID, Title, Description, Address, City, RentAmount, Status, 
                                                           Rooms, Kitchen, WashRoom, IsPetAllowed, IsAC, AvailabilityStatus) 
                                   OUTPUT INSERTED.PropertyID 
                                   VALUES (@LandlordID, @Title, @Description, @Address, @City, @RentAmount, @Status,
                                           @Rooms, @Kitchen, @WashRoom, @IsPetAllowed, @IsAC, @AvailabilityStatus)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@LandlordID", landlordId);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Description", description ?? "");
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@City", city);
                    cmd.Parameters.AddWithValue("@RentAmount", rentAmount);
                    cmd.Parameters.AddWithValue("@Status", status);

                    cmd.Parameters.AddWithValue("@Rooms", rooms);
                    cmd.Parameters.AddWithValue("@Kitchen", kitchen);
                    cmd.Parameters.AddWithValue("@WashRoom", washroom);
                    cmd.Parameters.AddWithValue("@IsPetAllowed", isPet);
                    cmd.Parameters.AddWithValue("@IsAC", isAc);
                    cmd.Parameters.AddWithValue("@AvailabilityStatus", availability);

                    cmd.CommandTimeout = 2;
                    conn.Open();
                    _cachedProperties = null;
                    int newId = (int)cmd.ExecuteScalar();

                    if (newId > 0 && imagePaths != null && imagePaths.Count > 0)
                    {
                        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        foreach (var path in imagePaths)
                        {
                            if (string.IsNullOrWhiteSpace(path)) continue;
                            var p = path.Trim();
                            if (!seen.Add(p)) continue;

                            string imgSql = "INSERT INTO PROPERTY_IMAGES (PropertyID, ImagePath) VALUES (@PID, @Path)";
                            SqlCommand imgCmd = new SqlCommand(imgSql, conn);
                            imgCmd.Parameters.AddWithValue("@PID", newId);
                            imgCmd.Parameters.AddWithValue("@Path", p);
                            imgCmd.ExecuteNonQuery();
                        }
                    }

                    return newId;
                }
            }
            catch (Exception ex)
            {
                DatabaseState.MarkFailed();
                throw new Exception("AddProperty Error: " + ex.Message, ex);
            }
        }

        public bool UpdateProperty(int propertyId, string title, string description, string address, string city, decimal rentAmount, string status,
                                   int rooms, int kitchen, int washroom, bool isPet, bool isAc, bool availability, List<string>? imagePaths = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string sql = @"UPDATE PROPERTIES SET Title=@Title, Description=@Description, Address=@Address, City=@City, 
                                   RentAmount=@RentAmount, Status=@Status, Rooms=@Rooms, Kitchen=@Kitchen, WashRoom=@WashRoom, 
                                   IsPetAllowed=@IsPetAllowed, IsAC=@IsAC, AvailabilityStatus=@AvailabilityStatus
                                   WHERE PropertyID=@PropertyID";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@PropertyID", propertyId);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Description", description ?? "");
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@City", city);
                    cmd.Parameters.AddWithValue("@RentAmount", rentAmount);
                    cmd.Parameters.AddWithValue("@Status", status);

                    cmd.Parameters.AddWithValue("@Rooms", rooms);
                    cmd.Parameters.AddWithValue("@Kitchen", kitchen);
                    cmd.Parameters.AddWithValue("@WashRoom", washroom);
                    cmd.Parameters.AddWithValue("@IsPetAllowed", isPet);
                    cmd.Parameters.AddWithValue("@IsAC", isAc);
                    cmd.Parameters.AddWithValue("@AvailabilityStatus", availability);

                    cmd.CommandTimeout = 30;
                    conn.Open();
                    _cachedProperties = null;
                    bool updated = cmd.ExecuteNonQuery() > 0;

                    if (updated && imagePaths != null && imagePaths.Count > 0)
                    {
                        SqlCommand delCmd = new SqlCommand("DELETE FROM PROPERTY_IMAGES WHERE PropertyID = @PID", conn);
                        delCmd.Parameters.AddWithValue("@PID", propertyId);
                        delCmd.ExecuteNonQuery();

                        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        foreach (var path in imagePaths)
                        {
                            if (string.IsNullOrWhiteSpace(path)) continue;
                            var p = path.Trim();
                            if (!seen.Add(p)) continue;

                            string imgSql = "INSERT INTO PROPERTY_IMAGES (PropertyID, ImagePath) VALUES (@PID, @Path)";
                            SqlCommand imgCmd = new SqlCommand(imgSql, conn);
                            imgCmd.Parameters.AddWithValue("@PID", propertyId);
                            imgCmd.Parameters.AddWithValue("@Path", p);
                            imgCmd.ExecuteNonQuery();
                        }
                    }

                    return updated;
                }
            }
            catch
            {
                DatabaseState.MarkFailed();
                return false;
            }
        }

        public bool DeleteProperty(int propertyId)
        {
            if (DatabaseState.ConnectionFailed) return false;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM PROPERTIES WHERE PropertyID = @PropertyID", conn);
                    cmd.Parameters.AddWithValue("@PropertyID", propertyId);
                    cmd.CommandTimeout = 2;
                    conn.Open();
                    _cachedProperties = null;
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                DatabaseState.MarkFailed();
                return false;
            }
        }

        public List<PropertyModel> GetRentedProperties(int tenantId)
        {
            // Bookings flow not in scope for this change; keep behavior unchanged.
            return new List<PropertyModel>();
        }

        public List<PropertyModel> SearchProperties(RentalSystemUI.Models.PropertySearchFilter filter)
        {
            filter ??= RentalSystemUI.Models.PropertySearchFilter.Default;
            if (DatabaseState.ConnectionFailed) return new List<PropertyModel>();

            try
            {
                List<PropertyModel> properties = new List<PropertyModel>();

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    var where = new List<string>();
                    string baseQuery = @"SELECT p.PropertyID, p.Title, p.Description, p.Address, p.City, p.RentAmount, p.Status, p.CreatedAt,
                                     (SELECT COUNT(*) FROM REVIEWS r WHERE r.PropertyID = p.PropertyID) AS ReviewCount,
                                     (SELECT TOP 1 ImagePath FROM PROPERTY_IMAGES pi WHERE pi.PropertyID = p.PropertyID) AS FirstImage,
                                     p.AvailabilityStatus, p.Rooms, p.Kitchen, p.WashRoom, p.IsPetAllowed, p.IsAC
                                     FROM PROPERTIES p";

                    if (filter.OnlyAvailable)
                    {
                        where.Add("p.Status = 'Available'");
                        where.Add("p.AvailabilityStatus = 1");
                    }

                    if (!string.IsNullOrWhiteSpace(filter.SearchText))
                    {
                        where.Add("(p.City LIKE @q OR p.Address LIKE @q OR p.Title LIKE @q)");
                    }

                    // Price: only apply when provided by UI.
                    if (filter.MaxMonthlyRent.HasValue)
                    {
                        where.Add("p.RentAmount <= @maxRent");
                    }

                    if (filter.Bedrooms.HasValue)
                    {
                        where.Add("p.Rooms >= @rooms");
                    }

                    if (filter.Kitchens.HasValue)
                    {
                        where.Add("p.Kitchen >= @kitchen");
                    }

                    if (filter.Washrooms.HasValue)
                    {
                        where.Add("p.WashRoom >= @wash");
                    }

                    if (filter.Corridors.HasValue)
                    {
                        where.Add("(COL_LENGTH('PROPERTIES','Corridors') IS NULL OR p.Corridors >= @corridors)");
                    }

                    if (filter.MinSquareFeet.HasValue && filter.MinSquareFeet.Value > 0)
                    {
                        where.Add("(COL_LENGTH('PROPERTIES','SquareFeet') IS NULL OR p.SquareFeet >= @sqft)");
                    }

                    // Pet filter: only apply when explicitly requested (UI sets to true)
                    if (filter.PetFriendly.HasValue && filter.PetFriendly.Value)
                    {
                        where.Add("p.IsPetAllowed = 1");
                    }

                    // AC filter removed from UI; ignore even if set for safety

                    string sql = baseQuery;
                    if (where.Count > 0)
                    {
                        sql += " WHERE " + string.Join(" AND ", where);
                    }
                    sql += " ORDER BY p.CreatedAt DESC";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandTimeout = 3;

                    if (!string.IsNullOrWhiteSpace(filter.SearchText))
                        cmd.Parameters.AddWithValue("@q", "%" + filter.SearchText.Trim() + "%");
                    if (filter.MaxMonthlyRent.HasValue)
                        cmd.Parameters.AddWithValue("@maxRent", filter.MaxMonthlyRent.Value);
                    if (filter.Bedrooms.HasValue)
                        cmd.Parameters.AddWithValue("@rooms", filter.Bedrooms.Value);
                    if (filter.Kitchens.HasValue)
                        cmd.Parameters.AddWithValue("@kitchen", filter.Kitchens.Value);
                    if (filter.Washrooms.HasValue)
                        cmd.Parameters.AddWithValue("@wash", filter.Washrooms.Value);
                    if (filter.Corridors.HasValue)
                        cmd.Parameters.AddWithValue("@corridors", filter.Corridors.Value);
                    if (filter.MinSquareFeet.HasValue && filter.MinSquareFeet.Value > 0)
                        cmd.Parameters.AddWithValue("@sqft", filter.MinSquareFeet.Value);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            properties.Add(new PropertyModel
                            {
                                PropertyID = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Address = reader.GetString(3),
                                City = reader.GetString(4),
                                RentAmount = reader.GetDecimal(5),
                                Status = reader.GetString(6),
                                CreatedAt = reader.GetDateTime(7),
                                ReviewCount = reader.GetInt32(8),
                                FirstImagePath = reader.IsDBNull(9) ? "" : reader.GetString(9),
                                AvailabilityStatus = !reader.IsDBNull(10) && reader.GetBoolean(10),
                                Rooms = !reader.IsDBNull(11) ? reader.GetInt32(11) : 0,
                                Kitchen = !reader.IsDBNull(12) ? reader.GetInt32(12) : 0,
                                WashRoom = !reader.IsDBNull(13) ? reader.GetInt32(13) : 0,
                                IsPetAllowed = !reader.IsDBNull(14) && reader.GetBoolean(14),
                                IsAC = !reader.IsDBNull(15) && reader.GetBoolean(15)
                            });
                        }
                    }
                }

                return properties;
            }
            catch
            {
                DatabaseState.MarkFailed();
                return new List<PropertyModel>();
            }
        }
    }

    public class PropertyModel
    {
        public int PropertyID { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public decimal RentAmount { get; set; }
        public string Status { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public int ReviewCount { get; set; }
        public string FirstImagePath { get; set; } = "";

        public bool AvailabilityStatus { get; set; }
        public int Rooms { get; set; }
        public int Kitchen { get; set; }
        public int WashRoom { get; set; }
        public bool IsPetAllowed { get; set; }
        public bool IsAC { get; set; }
    }

    public class ReviewModel
    {
        public int ReviewID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public string TenantName { get; set; } = "";
    }
}
