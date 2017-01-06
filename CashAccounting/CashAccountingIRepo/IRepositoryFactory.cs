using System;
using System.Collections.Generic;
using System.Data.Common;

namespace CashAccountingIRepo {
    public interface IRepositoryFactory {

        DbConnection MyDbConnection { get; set; }

        IBelegRepo GetBelegRepo();
        IFAKategorieRepo GetFAKagegorieRepo();
        IBankRepo GetBankRepo();

    }
}
