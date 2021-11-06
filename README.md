<h1>Booking API documentation and instructions</h1>
<hr>
"Booking" is an API service that makes the user able to make a reservation trips 
Booking API Created with .NET 5
<br/>

<ol>

  <li>Update ConnectionString on <code>appsettings.json</code> on BookingApi Project  By Change <code>DefaultConnection</code> value to your Connection String</li>
<li>Update ConnectionString on  <code>appsettings.json</code> on BookingMVC Project  By Change <code>DefaultConnection</code> value to your Connection String </li>
<li>Update ConnectionString on  <code>ApplicationDbContext.cs</code> on OnConfiguring  <code>options.UseSqlServer("Your Connection String");</code></li>
  <li>In Package Manager Console write command <code>update-database</code></li>
</ol>
