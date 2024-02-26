namespace ATM_C_BackEnd_ItAcademyStep_
{
    using System;
    using System.IO;

    class Program
    {
        static string filePath = "accounts.txt";

        static void Main(string[] args)
        {
            if (!File.Exists(filePath))
            {
                CreateDefaultFile();
            }

            while (true)
            {
                Console.WriteLine("Welcome to the ATM application!");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Deposit Money");
                Console.WriteLine("3. Withdraw Money");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            CheckBalance();
                            break;
                        case 2:
                            Deposit();
                            break;
                        case 3:
                            Withdraw();
                            break;
                        case 4:
                            Console.WriteLine("Thank you for using the ATM application. Goodbye!");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                }

                Console.WriteLine();
            }
        }

        static void CreateDefaultFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("dima,1000.00");
                    writer.WriteLine("gio,500.00");
           
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating default accounts file: {ex.Message}");
            }
        }

        static void CheckBalance()
        {
            Console.WriteLine("Checking balance...");
            string username = GetUsername();
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts[0] == username)
                    {
                        Console.WriteLine($"Your balance is: {parts[1]}");
                        return;
                    }
                }
                Console.WriteLine("User not found.");
                Console.Write("Would you like to add a new user? (yes/no): ");
                string response = Console.ReadLine().ToLower();
                if (response == "yes")
                {
                    AddNewUser(username);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading account data: {ex.Message}");
            }
        }

        static void AddNewUser(string username)
        {
            try
            {
                Console.Write("Enter initial balance for the new user: ");
                decimal balance;
                if (decimal.TryParse(Console.ReadLine(), out balance))
                {
                    using (StreamWriter writer = File.AppendText(filePath))
                    {
                        writer.WriteLine($"{username},{balance}");
                        Console.WriteLine("New user added successfully.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid balance. New user not added.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding new user: {ex.Message}");
            }
        }

        static void Deposit()
        {
            Console.WriteLine("Depositing money...");
            string username = GetUsername();
            Console.Write("Enter amount to deposit: ");
            decimal amount;
            if (decimal.TryParse(Console.ReadLine(), out amount))
            {
                try
                {
                    string[] lines = File.ReadAllLines(filePath);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] parts = lines[i].Split(',');
                        if (parts[0] == username)
                        {
                            decimal balance = decimal.Parse(parts[1]);
                            balance += amount;
                            lines[i] = $"{username},{balance}";
                            File.WriteAllLines(filePath, lines);
                            Console.WriteLine("Deposit successful.");
                            return;
                        }
                    }
                    Console.WriteLine("User not found.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating account data: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }

        static void Withdraw()
        {
            Console.WriteLine("Withdrawing money...");
            string username = GetUsername();
            Console.Write("Enter amount to withdraw: ");
            decimal amount;
            if (decimal.TryParse(Console.ReadLine(), out amount))
            {
                try
                {
                    string[] lines = File.ReadAllLines(filePath);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] parts = lines[i].Split(',');
                        if (parts[0] == username)
                        {
                            decimal balance = decimal.Parse(parts[1]);
                            if (balance >= amount)
                            {
                                balance -= amount;
                                lines[i] = $"{username},{balance}";
                                File.WriteAllLines(filePath, lines);
                                Console.WriteLine("Withdrawal successful.");
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Insufficient balance.");
                                return;
                            }
                        }
                    }
                    Console.WriteLine("User not found.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating account data: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }

        static string GetUsername()
        {
            Console.Write("Enter username: ");
            return Console.ReadLine();
        }
    }


}
