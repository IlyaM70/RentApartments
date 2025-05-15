<h1>🏢 RentApartments</h1>
<p>An ASP.NET Core Web API application designed for managing apartment rental listings, allowing users to browse, add, and manage apartment information.</p>

<h2>🚀 Features</h2>
<ul>
  <li>CRUD operations for apartment listings</li>
  <li>Property details including location, price, and amenities</li>
  <li>Search and filter functionality</li>
  <li>Authentication and authorization for users</li>
</ul>

<h2>🛠️ Technologies Used</h2>
<ul>
  <li>ASP.NET Core Web API</li>
  <li>Entity Framework Core</li>
  <li>SQL Server</li>
  <li>JWT Authentication</li>
  <li>Swagger for API documentation</li>
</ul>

<h2>📥 Installation</h2>
<ol>
  <li><strong>Clone the repository</strong>
    <pre><code>git clone https://github.com/IlyaM70/RentApartments.git
cd RentApartments</code></pre>
  </li>
  <li><strong>Open the solution in Visual Studio</strong>
    <ul>
      <li>Launch Visual Studio.</li>
      <li>Open the <code>RentApartments.sln</code> file.</li>
    </ul>
  </li>
  <li><strong>Configure the database</strong>
    <ul>
      <li>Update the connection string in <code>appsettings.json</code> to match your SQL Server configuration.</li>
      <li>Apply migrations to set up the database schema.</li>
    </ul>
  </li>
  <li><strong>Build and run the application</strong>
    <ul>
      <li>Build the solution to restore any necessary packages.</li>
      <li>Run the application using the debugger or press <code>F5</code>.</li>
    </ul>
  </li>
</ol>

<h2>💻 Usage</h2>
<p>After launching the application:</p>
<ul>
  <li>Access the API documentation via Swagger at <code>https://localhost:5001/swagger</code>.</li>
  <li>Use the provided endpoints to interact with apartment listings.</li>
  <li>Ensure you have a valid JWT token for endpoints requiring authentication.</li>
</ul>

<h2>📂 Project Structure</h2>
<pre><code>RentApartments/
├── RentApartments.API/       # API project
│   ├── Controllers/          # API controllers
│   ├── Models/               # Data models
│   └── Services/             # Business logic
├── RentApartments.Data/      # Data access layer
│   ├── Migrations/           # Database migrations
│   └── Repositories/         # Data repositories
├── RentApartments.sln        # Visual Studio solution file
└── README.md                 # Project documentation</code></pre>
