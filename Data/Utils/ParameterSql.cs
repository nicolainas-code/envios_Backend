namespace WebApi.Data.Utils
{
    public class ParameterSql
    {
        public ParameterSql(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }
}
