# Hashing
Securing a passwords in database using hashing


MD5 is a cryptographic function allows to make "hash" from any string taken as input. this function is irreversible, 
you can't go around and decode the hash to plain text.

Only way to decode a MD5 hash is to compare compare and compare the  hash against another hashes.
finding a hash like that is almost impossible it may be easy with a string like "abcd" or whatever a kiddy string.

MD5 is no longer considered a secure way to store passwords. sha256,512,bscrypt,scrypt are better they say.
as the time goes calculation power of computers are getting pretty high with that "Bruteforce Attacks" are getting easier 
day by day but not really easy.

Using a application like "salt" you can tighten the md5 hash its a string of characters that you can add to 
your username or password or whatever.

<span style="color:orange;">
Funtion to hash a string.

using System.Security.Cryptography

static string Encrypt(string value)//encrypting function
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }
        
 </span>  
 using System.Text.RegularExpressions;
 
 whatever the string your getting back from database after you saved it there = Regex.Replace(yourstringvariable, @"\s", "");
 
 //in database after you save a string ex: "vishwa" its 6 character and you give varchar(100) right what happen is it will be saved like vishwa+"94spaces" which is a pain
 this is the way i figured to remove that spaces.maybe theres a setting to stop keeping spaces i didn't figured yet?
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
