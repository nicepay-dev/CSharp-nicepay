
public class AccessTokenBuilder
{
    private string _grantType;
    private object _additionalInfo;

    public AccessTokenBuilder(string grantType)
        {
            _grantType = grantType;
            _additionalInfo = new { }; // Default empty object
        }

        public AccessTokenBuilder SetGrantType(string grantType)
        {
            _grantType = grantType;
            return this;
        }

        public AccessTokenBuilder SetAdditionalInfo(object additionalInfo)
        {
            _additionalInfo = additionalInfo;
            return this;
        }

        public object Build()
        {
            return new
            {
                grantType = _grantType,
                additionalInfo = _additionalInfo
            };
        }
}