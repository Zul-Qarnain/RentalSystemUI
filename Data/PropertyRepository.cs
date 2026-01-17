using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using RentalSystemUI.Models;

namespace RentalSystemUI.Data
{
    public class PropertyRepository : Database
    {
        public List<Property> GetByLandlordId(int landlordId)
        {
            var list = new List<Property>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"SELECT p.PropertyID, p.LandlordID, p.Title, p.Address, p.City, p.RentAmount, p.Description, p.Status,
                                       (SELECT TOP 1 ImagePath FROM PROPERTY_IMAGES pi WHERE pi.PropertyID = p.PropertyID) as CoverImage
                                FROM PROPERTIES p
                                WHERE p.LandlordID = @lid
                                ORDER BY p.CreatedAt DESC";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@lid", landlordId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Property
                            {
                                PropertyID = (int)reader["PropertyID"],
                                LandlordID = (int)reader["LandlordID"],
                                Title = reader["Title"].ToString() ?? "",
                                Address = reader["Address"].ToString() ?? "",
                                City = reader["City"].ToString() ?? "",
                                RentAmount = reader["RentAmount"] != DBNull.Value ? Convert.ToDecimal(reader["RentAmount"]) : 0,
                                Description = reader["Description"].ToString() ?? "",
                                Status = reader["Status"]?.ToString() ?? "Available",
                                CoverImage = reader["CoverImage"].ToString() ?? ""
                            });
                        }
                    }
                }
            }
            return list;
        }

        public List<Property> GetAllAvailable()
        {
            var list = new List<Property>();
            using (var conn = GetConnection())
            {
                conn.Open();
                // Matching the logic in RentAllSearch.cs:
                // SELECT p.PropertyID, p.Title, p.Address, p.RentAmount, (SELECT TOP 1 ImagePath FROM PROPERTY_IMAGES pi WHERE pi.PropertyID = p.PropertyID) as CoverImage FROM PROPERTIES p WHERE p.Status = 'Available'
                string query = @"SELECT p.PropertyID, p.Title, p.Address, p.RentAmount, 
                                (SELECT TOP 1 ImagePath FROM PROPERTY_IMAGES pi WHERE pi.PropertyID = p.PropertyID) as CoverImage 
                                FROM PROPERTIES p 
                                WHERE p.Status = 'Available'";

                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var p = new Property
                            {
                                PropertyID = (int)reader["PropertyID"],
                                Title = reader["Title"].ToString() ?? "",
                                Address = reader["Address"].ToString() ?? "",
                                RentAmount = Convert.ToDecimal(reader["RentAmount"]),
                                CoverImage = reader["CoverImage"].ToString() ?? ""
                            };
                            list.Add(p);
                        }
                    }
                }
            }
            return list;
        }

        public Property? GetById(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT PropertyID, Title, Address, City, RentAmount, Description, Status FROM PROPERTIES WHERE PropertyID = @pid";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@pid", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Property
                            {
                                PropertyID = id, // or (int)reader["PropertyID"]
                                Title = reader["Title"].ToString() ?? "",
                                Address = reader["Address"].ToString() ?? "",
                                City = reader["City"].ToString() ?? "",
                                RentAmount = reader["RentAmount"] != DBNull.Value ? Convert.ToDecimal(reader["RentAmount"]) : 0,
                                Description = reader["Description"].ToString() ?? "",
                                Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString()! : "Available"
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<PropertyImage> GetImagesByPropertyId(int propertyId)
        {
            var list = new List<PropertyImage>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT ImageID, PropertyID, ImagePath FROM PROPERTY_IMAGES WHERE PropertyID = @pid";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@pid", propertyId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new PropertyImage
                            {
                                ImageID = reader["ImageID"] != DBNull.Value ? (int)reader["ImageID"] : 0,
                                PropertyID = (int)reader["PropertyID"],
                                ImagePath = reader["ImagePath"].ToString() ?? ""
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
