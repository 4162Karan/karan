using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace JSProject.Models
{
    public class TestModel
    {
        public TestModel()
        {
            this.Checkbox = new List<string>();
        }
        public int Id { get; set; }
        public List<string> Checkbox { get; set; }

        public IFormFile FileName { get; set; }


       // [Required(ErrorMessage ="Role name is required")]
        [LocalizedRequired("NameRequired")]
        public string RoleName { get; set; }
    }
}


public class LocalizedRequiredAttribute : RequiredAttribute
{
    private readonly string _resourceKey;

    public LocalizedRequiredAttribute(string resourceKey)
    {
        _resourceKey = resourceKey;
    }

    public override string FormatErrorMessage(string name)
    {
        // Get the localized error message from the database using the resource key
        string errorMessage = GetErrorMessageFromDatabase(_resourceKey);

        // Replace the {0} placeholder with the name of the property
        return string.Format(errorMessage, name);
    }

    private string GetErrorMessageFromDatabase(string resourceKey)
    {
        // TODO: Retrieve the localized error message from the database using the resource key
        // You can use any method you prefer to retrieve the error message from the database
        // This example assumes that you have a table called "ErrorMessages" with columns "ResourceKey" and "Message"
        string connectionString = "your connection string here";
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var command = new SqlCommand("SELECT Message FROM ErrorMessages WHERE ResourceKey = @ResourceKey", connection);
            command.Parameters.AddWithValue("@ResourceKey", resourceKey);
            return (string)command.ExecuteScalar();
        }
    }
}
