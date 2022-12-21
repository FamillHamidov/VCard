using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VCard_Task_21.Models;

public class Program
{
    public static void Main()
    {
        var client = new HttpClient();
        var request = client.GetAsync("https://randomuser.me/api?results=2").Result;
        var response = request.Content.ReadAsStringAsync().Result;
        JObject json = JObject.Parse(response);
        foreach (var item in json["results"])
        {
            User user1 = new User();
            user1.FirstName = item["name"]["first"].ToString();
            user1.SurName = item["name"]["last"].ToString();
            user1.Email = item["email"].ToString();
            user1.Phone = item["phone"].ToString();
            user1.Country = item["location"]["country"].ToString();
            user1.City = item["location"]["city"].ToString();
            ConvertVcard(user1);
        }
    }

    //Users data convert to VCard 
    public static void ConvertVcard(User user)
    {
        string vcard= $@"
                   BEGIN:VCARD
                   FN: {user.FirstName} {user.SurName}
                   Email: {user.Email}
                   Phone: {user.Phone}
                   Country: {user.Country}
                   City: {user.City}
                   END:VCARD";
        Console.WriteLine(vcard);

        //Unique filename
        var filename = $@"{user.FirstName+user.SurName+DateTime.Now.Ticks}.txt";
        string path = $@"C:\Users\famil\source\repos\VCard_Task\VCard_Task_21\Vcards\{filename}";
        FileStream fileStream=new FileStream(path, FileMode.Create);
        StreamWriter streamWriter = new StreamWriter(fileStream);
        streamWriter.WriteLine(vcard);
        streamWriter.Close();
        fileStream.Close();
        Console.WriteLine("Fayl yaradildi");
    }

   
}