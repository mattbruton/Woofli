using System;
using System.Collections.Generic;
using Typesafe.Mailgun;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using woofli_be_v2._0.DAL;
using woofli_be_v2._0.Models;

namespace woofli_SMS
{
    class Program
    {
        public static bool runProgram = true;



        static void Main(string[] args)
        {
            while (runProgram)
            {
                AuthRepository repo = new AuthRepository();
                List<CustomUser> users = repo.GetAllUsers();

                foreach (var user in users)
                {
                    foreach (var pet in user.Pets)
                    {
                        if (pet.Medications.Any(m => m.DosageTime > DateTime.Now && m.DosageTime < DateTime.Now.AddHours(1)))
                        {
                            List<Medicine> meds_with_upcoming_message = pet.Medications.Where((m => m.DosageTime > DateTime.Now && m.DosageTime < DateTime.Now.AddMinutes(10))).ToList();

                            foreach (var med in meds_with_upcoming_message)
                            {
                                var client = new MailgunClient("sandboxb97fd526243f498e99be4b242a0c8b43.mailgun.org", SecretKey.ApiKey, 3);

                                client.SendMail(new System.Net.Mail.MailMessage("reminder@woof.li", user.Email)
                                {
                                    Subject = "Medication Reminder for " + pet.Name,
                                    Body = pet.Name + " needs their dose of " + med.Name + " at " + med.DosageTime.ToShortTimeString() + "."
                                });

                                Console.WriteLine(pet.Name + " needs his or her next dose of " + med.Name + " at " + med.DosageTime.ToShortTimeString() + " -- From woof.li");
                            }
                        }
                    }
                }

                Thread.Sleep(10 * 60 * 1000);
            }
            
        }
    }
}
