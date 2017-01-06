using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashAccountingEntities;

namespace CashAccountingIRepo {
    public interface IBankRepo {

        #region Banken

        List<Bank> GetAllBanken();
        Bank GetBankById(int bankId);
        Bank GetBankenByBIC(string bic);
        Bank GetBankZuKonto(Bankkonto bankkonto);
        int SaveBank(Bank bank);

        #endregion

        #region Bankkonten

        List<Bankkonto> GetAllBankkonten();
        Bankkonto GetBankkontoByIBAN(string IBAN);
        Bankkonto GetBankkontoByKtoNr(string p, int bankId);
        Bankkonto GetBankkontoById(int kontoId);

        #endregion

        #region Bankkontobewegungen

        Bankkontobewegung FindDuplicateBankkontobewegung(Bankkontobewegung buchung);
        int ImportiereBankkontobewegung(Bankkontobewegung buchung);
        int SichereBankbuchung(Bankkontobewegung buchung);
        List<Bankkontobewegung> GetBankkontobewegungNotInFiBu();
        List<Bankkontobewegung> GetBankkontobewegungByQueryData(BankkontobewegungQueryData qryDta);
        List<Bankbewegungstyp> GetAllAbgabentypen();

        #endregion

    }
}
