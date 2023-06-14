using System.ComponentModel.DataAnnotations;

namespace ConnectFourGame.API.Utilities
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
           AllowMultiple = false)]
    public class IdExists : ValidationAttribute
    {
        private readonly string _database;

        public IdExists(string database)
        { 
            _database = database;
        }
        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage("The given Id is already exists in the database");
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            //TODO: Impliment when a db will be added 
            return ValidationResult.Success;



        }
    }
}
