<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Project Blueprint: RentSphere</title>
    <script src="https://cdn.tailwindcss.com"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@300;400;500;700&display=swap" rel="stylesheet">
    <style>
        body { font-family: 'Roboto', sans-serif; background-color: #F3F4F6; color: #1F2937; }
        .chart-container { 
            position: relative; 
            width: 100%; 
            max-width: 600px; 
            margin-left: auto; 
            margin-right: auto; 
            height: 300px; 
            max-height: 400px; 
        }
        @media (min-width: 768px) { .chart-container { height: 350px; } }
        
        /* Flowchart Connector Styles - Pure CSS */
        .flow-step { position: relative; }
        .flow-arrow::after {
            content: '↓';
            display: block;
            text-align: center;
            font-size: 24px;
            color: #2563EB; /* Blue-600 */
            margin: 10px 0;
            font-weight: bold;
        }
        .last-step::after { content: none; }

        /* Custom Scrollbar */
        ::-webkit-scrollbar { width: 8px; }
        ::-webkit-scrollbar-track { background: #E5E7EB; }
        ::-webkit-scrollbar-thumb { background: #3B82F6; border-radius: 4px; }
        ::-webkit-scrollbar-thumb:hover { background: #2563EB; }
    </style>
    <!-- 
    Palette Name: Energetic Blue & Vibrant Tech
    Colors: 
    - Primary: #3B82F6 (Blue-500)
    - Dark: #1E3A8A (Blue-900)
    - Accent: #F59E0B (Amber-500)
    - Background: #F3F4F6 (Gray-100)
    - Surface: #FFFFFF (White)
    -->
    <!-- 
    Plan Narrative:
    1. Introduction: Project Name & Core Concept.
    2. User Architecture: Visualizing the 3 key roles.
    3. Feature Breakdown: Charting complexity by role.
    4. Database Schema: Visualizing the 5 tables.
    5. Application Structure: Form count & workflow.
    -->
    <!-- 
    Visualization Choices:
    1. User Roles -> Goal: Inform -> Doughnut Chart -> Shows clear separation of the 3 user types. (NO SVG)
    2. Feature Count -> Goal: Compare -> Bar Chart -> Compares functionality load between roles. (NO SVG)
    3. Form Distribution -> Goal: Organize -> Polar Area Chart -> Shows where the development effort lies in terms of UI forms. (NO SVG)
    4. Database Schema -> Goal: Relationships -> CSS Grid Cards -> Structured HTML to show tables and fields without SVG diagrams. (NO SVG)
    5. Booking Process -> Goal: Organize -> CSS Flowchart -> Step-by-step text blocks with CSS arrows. (NO SVG)
    -->
    <!-- 
    NO MERMAID JS USED. 
    NO SVG USED.
    -->
</head>
<body class="bg-gray-100">

    <!-- Navigation / Header -->
    <header class="bg-blue-600 text-white shadow-lg sticky top-0 z-50">
        <div class="container mx-auto px-6 py-4 flex justify-between items-center">
            <h1 class="text-2xl font-bold tracking-wide">RentSphere <span class="text-blue-200 text-sm font-normal">| C# Project Blueprint</span></h1>
            <nav class="hidden md:flex space-x-6 text-sm font-medium">
                <a href="#overview" class="hover:text-blue-200 transition">Overview</a>
                <a href="#roles" class="hover:text-blue-200 transition">Users</a>
                <a href="#database" class="hover:text-blue-200 transition">Database</a>
                <a href="#ui-forms" class="hover:text-blue-200 transition">UI Design</a>
            </nav>
        </div>
    </header>

    <main class="container mx-auto px-4 py-8 space-y-12">

        <!-- 1. Project Identity Section -->
        <section id="overview" class="grid grid-cols-1 md:grid-cols-2 gap-8 items-center bg-white rounded-xl shadow-md p-8 border-l-8 border-blue-500">
            <div>
                <h2 class="text-4xl font-extrabold text-gray-800 mb-4">Project Identity</h2>
                <p class="text-gray-600 text-lg mb-6">
                    You asked for a name and a vision. The chosen project title is <strong>RentSphere</strong>. 
                    It represents a comprehensive ecosystem for landlords and tenants. This section outlines the high-level scope 
                    before diving into the technical details.
                </p>
                <div class="grid grid-cols-2 gap-4">
                    <div class="bg-blue-50 p-4 rounded-lg">
                        <span class="block text-blue-600 font-bold text-xl">Platform</span>
                        <span class="text-gray-700">Windows (WinForms/WPF)</span>
                    </div>
                    <div class="bg-blue-50 p-4 rounded-lg">
                        <span class="block text-blue-600 font-bold text-xl">Language</span>
                        <span class="text-gray-700">C# .NET</span>
                    </div>
                    <div class="bg-blue-50 p-4 rounded-lg">
                        <span class="block text-blue-600 font-bold text-xl">Database</span>
                        <span class="text-gray-700">MSSQL / MySQL</span>
                    </div>
                    <div class="bg-blue-50 p-4 rounded-lg">
                        <span class="block text-blue-600 font-bold text-xl">Target</span>
                        <span class="text-gray-700">University Final Project</span>
                    </div>
                </div>
            </div>
            <div class="bg-gradient-to-br from-blue-600 to-blue-800 text-white p-8 rounded-xl shadow-lg flex flex-col justify-center items-center text-center">
                <div class="text-6xl mb-4">🏠</div>
                <h3 class="text-3xl font-bold mb-2">RentSphere</h3>
                <p class="text-blue-100">Smart Home Rental Management System</p>
                <div class="mt-6 text-sm bg-white/20 py-2 px-4 rounded-full">
                    "Connecting Tenants to Homes, Landlords to Ease."
                </div>
            </div>
        </section>

        <!-- 2. User Architecture & Features -->
        <section id="roles">
            <div class="mb-6">
                <h2 class="text-3xl font-bold text-gray-800 border-b-4 border-blue-500 inline-block pb-2">User Roles & Feature Distribution</h2>
                <p class="mt-4 text-gray-600 max-w-3xl">
                    Your system is defined by three distinct actors. The chart below visualizes the functional load (number of distinct capabilities) for each user type. 
                    This helps in planning your coding effort.
                </p>
            </div>

            <div class="grid grid-cols-1 md:grid-cols-2 gap-8">
                <!-- Chart: Feature Complexity -->
                <div class="bg-white p-6 rounded-xl shadow-md">
                    <h3 class="text-xl font-bold text-gray-700 mb-4 text-center">Functional Complexity per Role</h3>
                    <div class="chart-container">
                        <canvas id="featuresChart"></canvas>
                    </div>
                    <p class="text-xs text-gray-400 text-center mt-2">Represents the number of distinct actions (Create, Read, Update, Delete, etc.)</p>
                </div>

                <!-- Detailed Breakdown List -->
                <div class="bg-white p-6 rounded-xl shadow-md space-y-6">
                    
                    <!-- Tenant -->
                    <div class="border-l-4 border-green-500 pl-4">
                        <h4 class="font-bold text-lg text-green-700">👤 Tenant (The Customer)</h4>
                        <ul class="list-disc list-inside text-sm text-gray-600 mt-2 space-y-1">
                            <li><strong>Auth:</strong> Sign up, Login, Reset Password.</li>
                            <li><strong>Search:</strong> Filter by Location, Price, Room count.</li>
                            <li><strong>Booking:</strong> Select Duration (1-24m), View Total.</li>
                            <li><strong>Payment:</strong> QR Code simulation, Confirmation.</li>
                        </ul>
                    </div>

                    <!-- Landlord -->
                    <div class="border-l-4 border-blue-500 pl-4">
                        <h4 class="font-bold text-lg text-blue-700">🔑 Landlord (The Provider)</h4>
                        <ul class="list-disc list-inside text-sm text-gray-600 mt-2 space-y-1">
                            <li><strong>Manage Properties:</strong> Add, Edit, Delete listings.</li>
                            <li><strong>Media:</strong> Upload max 4 images per home.</li>
                            <li><strong>Financials:</strong> View payment status on homepage.</li>
                            <li><strong>Status:</strong> Approve/Reject booking requests.</li>
                        </ul>
                    </div>

                    <!-- Super Admin -->
                    <div class="border-l-4 border-purple-500 pl-4">
                        <h4 class="font-bold text-lg text-purple-700">🛡️ Super Admin (The Controller)</h4>
                        <ul class="list-disc list-inside text-sm text-gray-600 mt-2 space-y-1">
                            <li><strong>Oversight:</strong> View all Users & Landlords.</li>
                            <li><strong>Analytics:</strong> Track total sales & active bookings.</li>
                            <li><strong>Moderation:</strong> Delete/Ban suspicious users.</li>
                        </ul>
                    </div>
                </div>
            </div>
        </section>

        <!-- 3. Database Design (The Core Request) -->
        <section id="database" class="bg-gray-800 text-white rounded-xl shadow-xl p-8">
            <div class="mb-8 text-center">
                <h2 class="text-3xl font-bold text-blue-400 mb-2">Database Schema Design</h2>
                <p class="text-gray-300">
                    Based on your requirements, you need exactly <strong>5 Core Tables</strong>. 
                    This relational design ensures data integrity for users, listings, and financial transactions.
                </p>
            </div>

            <!-- Database Tables Visualization (CSS Cards) -->
            <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                
                <!-- Table: Users -->
                <div class="bg-gray-700 rounded-lg p-5 border-t-4 border-blue-500 hover:bg-gray-600 transition">
                    <div class="flex justify-between items-center mb-3">
                        <h4 class="font-bold text-xl">Users</h4>
                        <span class="text-xs bg-blue-500 px-2 py-1 rounded">PK: UserID</span>
                    </div>
                    <ul class="text-sm text-gray-300 space-y-2 font-mono">
                        <li>UserID (INT, Auto)</li>
                        <li>Username (VARCHAR)</li>
                        <li>Email (VARCHAR)</li>
                        <li>PasswordHash (VARCHAR)</li>
                        <li class="text-yellow-400">Role (Enum: Admin, Landlord, Tenant)</li>
                    </ul>
                </div>

                <!-- Table: Properties -->
                <div class="bg-gray-700 rounded-lg p-5 border-t-4 border-green-500 hover:bg-gray-600 transition">
                    <div class="flex justify-between items-center mb-3">
                        <h4 class="font-bold text-xl">Properties</h4>
                        <span class="text-xs bg-green-500 px-2 py-1 rounded">PK: PropertyID</span>
                    </div>
                    <ul class="text-sm text-gray-300 space-y-2 font-mono">
                        <li>PropertyID (INT)</li>
                        <li class="text-blue-300">LandlordID (FK -> Users)</li>
                        <li>Location (VARCHAR)</li>
                        <li>PricePerMonth (DECIMAL)</li>
                        <li>RoomCount (INT)</li>
                        <li>Status (Available/Booked)</li>
                    </ul>
                </div>

                <!-- Table: PropertyImages -->
                <div class="bg-gray-700 rounded-lg p-5 border-t-4 border-teal-500 hover:bg-gray-600 transition">
                    <div class="flex justify-between items-center mb-3">
                        <h4 class="font-bold text-xl">PropertyImages</h4>
                        <span class="text-xs bg-teal-500 px-2 py-1 rounded">PK: ImageID</span>
                    </div>
                    <ul class="text-sm text-gray-300 space-y-2 font-mono">
                        <li>ImageID (INT)</li>
                        <li class="text-green-300">PropertyID (FK -> Properties)</li>
                        <li>ImagePath (VARCHAR)</li>
                        <li class="text-gray-400 text-xs mt-2 italic">// Limit logic handled in code (Max 4)</li>
                    </ul>
                </div>

                <!-- Table: Bookings -->
                <div class="bg-gray-700 rounded-lg p-5 border-t-4 border-orange-500 hover:bg-gray-600 transition">
                    <div class="flex justify-between items-center mb-3">
                        <h4 class="font-bold text-xl">Bookings</h4>
                        <span class="text-xs bg-orange-500 px-2 py-1 rounded">PK: BookingID</span>
                    </div>
                    <ul class="text-sm text-gray-300 space-y-2 font-mono">
                        <li>BookingID (INT)</li>
                        <li class="text-blue-300">TenantID (FK -> Users)</li>
                        <li class="text-green-300">PropertyID (FK -> Properties)</li>
                        <li>DurationMonths (INT)</li>
                        <li>StartDate (DATE)</li>
                        <li>TotalAmount (DECIMAL)</li>
                    </ul>
                </div>

                <!-- Table: Payments -->
                <div class="bg-gray-700 rounded-lg p-5 border-t-4 border-purple-500 hover:bg-gray-600 transition">
                    <div class="flex justify-between items-center mb-3">
                        <h4 class="font-bold text-xl">Payments</h4>
                        <span class="text-xs bg-purple-500 px-2 py-1 rounded">PK: PaymentID</span>
                    </div>
                    <ul class="text-sm text-gray-300 space-y-2 font-mono">
                        <li>PaymentID (INT)</li>
                        <li class="text-orange-300">BookingID (FK -> Bookings)</li>
                        <li>AmountPaid (DECIMAL)</li>
                        <li>PaymentDate (DATETIME)</li>
                        <li>Method (Card/Bkash)</li>
                        <li>TransactionID (VARCHAR)</li>
                    </ul>
                </div>
                
                <!-- Summary Card -->
                <div class="bg-gray-800 border-2 border-dashed border-gray-600 rounded-lg p-5 flex flex-col justify-center items-center text-center">
                    <span class="text-5xl mb-2">🗄️</span>
                    <h4 class="font-bold text-lg">Total Tables: 5</h4>
                    <p class="text-sm text-gray-400">Efficient 3rd Normal Form Design</p>
                </div>

            </div>
        </section>

        <!-- 4. UI Forms & Logic (The "Surprise View") -->
        <section id="ui-forms">
            <div class="mb-6">
                <h2 class="text-3xl font-bold text-gray-800 border-b-4 border-blue-500 inline-block pb-2">UI Forms & Application Flow</h2>
                <p class="mt-4 text-gray-600">
                    To answer "How many forms?", we estimate <strong>10 to 12 distinct forms</strong> (or Views) are needed for a professional finish.
                    The chart below distributes the development effort across different modules.
                </p>
            </div>

            <div class="grid grid-cols-1 lg:grid-cols-3 gap-8">
                <!-- Chart: Form Distribution -->
                <div class="lg:col-span-1 bg-white p-6 rounded-xl shadow-md">
                    <h3 class="text-xl font-bold text-gray-700 mb-4 text-center">UI Development Effort</h3>
                    <div class="chart-container">
                        <canvas id="formsChart"></canvas>
                    </div>
                </div>

                <!-- Flowchart: The User Journey -->
                <div class="lg:col-span-2 bg-white p-6 rounded-xl shadow-md">
                    <h3 class="text-xl font-bold text-gray-700 mb-6">Standard Booking Logic Flow</h3>
                    
                    <div class="flex flex-col md:flex-row justify-between items-center space-y-4 md:space-y-0 md:space-x-4">
                        <!-- Step 1 -->
                        <div class="flow-step flow-arrow w-full md:w-1/4 bg-blue-50 border border-blue-200 p-4 rounded-lg text-center">
                            <div class="text-2xl mb-2">🔍</div>
                            <h4 class="font-bold text-blue-800">1. Search</h4>
                            <p class="text-xs text-gray-600">Tenant filters by Price, Room, City.</p>
                        </div>

                        <!-- Step 2 -->
                        <div class="flow-step flow-arrow w-full md:w-1/4 bg-blue-50 border border-blue-200 p-4 rounded-lg text-center">
                            <div class="text-2xl mb-2">📄</div>
                            <h4 class="font-bold text-blue-800">2. Select & Detail</h4>
                            <p class="text-xs text-gray-600">View Images, select duration (1-24m).</p>
                        </div>

                        <!-- Step 3 -->
                        <div class="flow-step flow-arrow w-full md:w-1/4 bg-blue-50 border border-blue-200 p-4 rounded-lg text-center">
                            <div class="text-2xl mb-2">💳</div>
                            <h4 class="font-bold text-blue-800">3. Payment</h4>
                            <p class="text-xs text-gray-600">Enter details, Generate QR, Confirm.</p>
                        </div>

                        <!-- Step 4 -->
                        <div class="flow-step last-step w-full md:w-1/4 bg-green-50 border border-green-200 p-4 rounded-lg text-center">
                            <div class="text-2xl mb-2">✅</div>
                            <h4 class="font-bold text-green-800">4. Confirmation</h4>
                            <p class="text-xs text-gray-600">Database updates, Landlord notified.</p>
                        </div>
                    </div>
                    
                    <div class="mt-8 bg-gray-50 p-4 rounded border border-gray-200">
                        <h4 class="font-bold text-gray-700 mb-2">Required Forms List:</h4>
                        <div class="grid grid-cols-2 gap-2 text-sm text-gray-600">
                            <div>1. LoginForm (All)</div>
                            <div>2. RegisterForm (User/Landlord)</div>
                            <div>3. TenantDashboard (Search/List)</div>
                            <div>4. PropertyDetailForm (Info & Images)</div>
                            <div>5. BookingForm (Duration & Calc)</div>
                            <div>6. PaymentGatewayForm (Mockup)</div>
                            <div>7. LandlordDashboard (Stats)</div>
                            <div>8. AddPropertyForm (Uploads)</div>
                            <div>9. AdminDashboard (User List)</div>
                            <div>10. SalesReportForm (Admin)</div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <!-- Footer -->
        <footer class="mt-12 text-center text-gray-500 text-sm pb-8">
            <p>Generated for RentSphere C# Project Planning</p>
            <p class="mt-2">Use this blueprint to structure your database and WinForms/WPF UI.</p>
        </footer>

    </main>

    <script>
        // Data Configuration for Feature Complexity Chart
        const featureCtx = document.getElementById('featuresChart').getContext('2d');
        const featuresChart = new Chart(featureCtx, {
            type: 'bar',
            data: {
                labels: ['Tenant', 'Landlord', 'Super Admin'],
                datasets: [{
                    label: 'Feature Count',
                    data: [8, 6, 4], // Estimated weighted complexity
                    backgroundColor: [
                        'rgba(52, 211, 153, 0.7)', // Green (Tenant)
                        'rgba(59, 130, 246, 0.7)', // Blue (Landlord)
                        'rgba(139, 92, 246, 0.7)'  // Purple (Admin)
                    ],
                    borderColor: [
                        'rgba(52, 211, 153, 1)',
                        'rgba(59, 130, 246, 1)',
                        'rgba(139, 92, 246, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    y: {
                        beginAtZero: true,
                        title: { display: true, text: 'Complexity Score' }
                    }
                },
                plugins: {
                    legend: { display: false },
                    tooltip: {
                        callbacks: {
                            title: function(tooltipItems) {
                                let label = tooltipItems[0].label;
                                if (label.length > 16) {
                                    return label.split(' '); // Wrap logic mock
                                }
                                return label;
                            }
                        }
                    }
                }
            }
        });

        // Data Configuration for Form Distribution Chart
        const formsCtx = document.getElementById('formsChart').getContext('2d');
        const formsChart = new Chart(formsCtx, {
            type: 'doughnut',
            data: {
                labels: ['Auth Forms', 'Tenant UI', 'Landlord UI', 'Admin UI'],
                datasets: [{
                    data: [2, 4, 3, 2], // 2 Auth, 4 Tenant, 3 Landlord, 2 Admin
                    backgroundColor: [
                        '#9CA3AF', // Gray
                        '#34D399', // Green
                        '#3B82F6', // Blue
                        '#8B5CF6'  // Purple
                    ],
                    borderWidth: 0
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: { position: 'bottom' },
                    tooltip: {
                        callbacks: {
                            title: function(tooltipItems) {
                                const item = tooltipItems[0];
                                let label = item.chart.data.labels[item.dataIndex];
                                if (Array.isArray(label)) return label.join(' ');
                                return label;
                            }
                        }
                    }
                }
            }
        });
    </script>
</body>
</html>