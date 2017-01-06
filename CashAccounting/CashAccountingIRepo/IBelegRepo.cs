using System;
using System.Collections.Generic;
using CashAccountingEntities;

namespace CashAccountingIRepo {
    public interface IBelegRepo {


        #region Beleg

        int SaveBeleg(Beleg beleg);

        void RemoveBelegById(int belegId);

        Beleg FindBelegById(int belegId, bool includeBelegPosition);

        List<Beleg> GetBelegByQueryData(BelegQueryData queryData, bool includeBelegPosition, bool includePayment);

        void UpdateBelegkopf(Beleg beleg);



        int SaveBelegPosition(int belegId, Belegposition belegPosition);

        void RemoveBelegPositionById(int belegPositionId);

        List<Belegposition> FindBelegPositionenByBeleg(Beleg beleg);

        List<Belegposition> FindBelegPositionenByBelegId(int belegId);



        int SaveBelegZahlung(int belegId, BelegZahlungKopf belegZahlung);

        void RemoveBelegZahlungById(int belegZahlungId);

        List<BelegZahlungKopf> FindBelegZahlungByBeleg(Beleg beleg);

        List<BelegZahlungKopf> FindBelegZahlungByBelegId(int belegId);



        int SaveBelegZahlungPosition(int zahlungId, BelegZahlungPosition belegZahlungPosition);

        void RemoveBelegZahlungPositionById(int belegZahlungPositionId);

        List<BelegZahlungPosition> FindBelegZahlungPositionByZahlung(BelegZahlungKopf belegZahlung);

        List<BelegZahlungPosition> FindBelegZahlungPositionZahlungId(int zahlungId);




        List<Land> GetLandAll();

        #endregion

    }
}
