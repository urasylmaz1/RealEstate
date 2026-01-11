using System;

namespace RealEstate.Business.Configs;

public class JwtConfig
{
    public string Secret { get; set; }=string.Empty;
    public string Issuer { get; set; }=string.Empty;
    public string Audience { get; set; }=string.Empty;
    public double AccessTokenExpiration { get; set; }
}
