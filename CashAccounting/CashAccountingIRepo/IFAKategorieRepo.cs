using System;
using System.Collections.Generic;
using CashAccountingEntities;

namespace CashAccountingIRepo {
    public interface IFAKategorieRepo {


        int Add(FAKategorie faKategorie);

        void Update(FAKategorie faKategorie);

        void RemoveById(int id);

        FAKategorie FindFAKategorieById(int id);

        List<FAKategorie> GetFAKategorieByQueryData(FAKategorieQueryData queryData);

    }
}
