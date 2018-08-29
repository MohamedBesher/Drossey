using System;

namespace Drossey.Admin.Services
{
    public interface IPinCodeGenerator
    {
        Tuple<string, string, string,double> Encrypt(int amount);


        string GetCode(int amount, double rand, string ivAsBase64, string keyAsBase64);

        //string Decrypt(string Decrptedkey);



    }
}
