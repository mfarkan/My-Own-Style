using Core.Enumarations;

namespace Domain.Model.Account
{
    public class BankAccount : EntityBase
    {
        public virtual string BankAccountName { get; set; }
        public virtual string BankAccountDescription { get; set; }
        public virtual BankType BankType { get; set; }
        public virtual decimal TotalBalance { get; set; }
        public virtual string AccountIBAN { get; set; }
        public virtual AccountType AccountType { get; set; }
        public virtual string AccountNo { get; set; }
        public virtual CurrencyType CurrencyType { get; set; }
        public virtual decimal UsableBalance { get; set; }
        public virtual decimal BlockedBalance { get; set; }
        public virtual Institution.Institution Institution { get; set; }
        public bool IsKMH() => AccountType == AccountType.Overdraft;
        public bool IsVadesiz() => AccountType == AccountType.Vadesiz;
        public bool IsVadeli() => AccountType == AccountType.Vadeli;
        public bool IsTurkishLira() => CurrencyType == CurrencyType.TRY;
        public bool IsCreditCard() => AccountType == AccountType.CreditCard;
    }
}
