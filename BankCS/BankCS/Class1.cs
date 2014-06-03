using System;

namespace BankAccountNS
{
    /// <summary> 
    /// Bank Account demo class. 
    /// </summary> 
    public class BankAccount
    {

        private bool frozenValue = false;

        private BankAccount()
        {
        }

        public BankAccount(string customerName, double balance)
        {
            CustomerName = customerName;
            Balance = balance;
        }

        public string CustomerName { get; private set; }


        public double Balance { get; private set; }

        public void Debit(double amount)
        {
            if (frozenValue)
            {
                throw new Exception("Account frozen");
            }

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            Balance -= amount;
        }

        public void Credit(double amount)
        {
            if (frozenValue)
            {
                throw new Exception("Account frozen");
            }

            if (amount > Balance)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            Balance += amount;
        }

        private void FreezeAccount()
        {
            frozenValue = true;
        }

        private void UnfreezeAccount()
        {
            frozenValue = false;
        }

        public static void Main()
        {
            BankAccount ba = new BankAccount("Mr. Bryan Walton", 11.99);

            ba.Credit(5.77);
            ba.Debit(11.22);
            Console.WriteLine("Current balance is ${0}", ba.Balance);
        }

    }
}
