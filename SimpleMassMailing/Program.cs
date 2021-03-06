﻿using SimpleMassMailing.Data;
using System;
using System.Linq;

namespace SimpleMassMailing
{
    public class Program
    {
        static void Main(string[] args)
        {
            const int confirmationPosition = 80;

            var config = new Configuration(args);
            var content = System.IO.File.ReadAllText(config.ContentFile);
            var dataRows = System.IO.File.ReadAllLines(config.DataFile).Select(row => new DataRow(row)).Where(row => !string.IsNullOrEmpty(row.EMail));
            var mail = new OutlookDotComMail(config);

            int rowNumber = 0;
            int rowTotal = dataRows.Count();
            foreach (var row in dataRows)
            {
                rowNumber++;
                string confirmation = $"{rowNumber}/{rowTotal}. Send to {row.EMail.Left(confirmationPosition - 22)} (Y/N)? ";
                Console.Write(confirmation);
                if (row.IsEnabled)
                {
                    bool toSend = false;

                    // Automatic sends
                    if (config.PromptBeforeSend)
                    {
                        var keyInfo = Console.ReadKey();
                        if (keyInfo.KeyChar == 'Y' || keyInfo.KeyChar == 'y')
                            toSend = true;
                    }
                    else
                    {
                        toSend = true;
                    }

                    if (toSend)
                    {
                        try
                        {
                            mail.SendMail(row.EMail, content, row.Parameters);
                            Console.WriteLine($"{String.Empty.PadLeft(confirmationPosition - confirmation.Length, ' ')} ... Sent");
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                        }                                               

                        // Wait some seconds before the next sent
                        if (config.PromptBeforeSend == false)
                        {
                            int timeBetweenNext = new Random().Next(500, 2000);
                            System.Threading.Thread.Sleep(timeBetweenNext);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{String.Empty.PadLeft(confirmationPosition - confirmation.Length, ' ')} ... Skipped");
                    }
                }
                else
                {
                    Console.WriteLine($"{String.Empty.PadLeft(confirmationPosition - confirmation.Length, ' ')}  ... Commented");
                }
            }

            Console.WriteLine("Completed");
        }


    }
}
