using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Enumarations
{
    public enum CurrencyType
    {
        [Display(Name = "AMERİKAN DOLARI")]
        USD = 1,

        [Display(Name = "KUVEYT DİNARI")]
        KWD = 2,

        [Display(Name = "KANADA DOLARI")]
        CAD = 3,

        [Display(Name = "AVUSTRALYA DOLARI")]
        AUD = 4,

        [Display(Name = "BULGAR LEVASI")]
        BGL = 5,

        [Display(Name = "100 IRAN RİYALİ")]
        IRR = 6,

        [Display(Name = "RUMEN LEYI")]
        RON = 7,

        [Display(Name = "SURİYE LİRASI")]
        SYP = 8,

        [Display(Name = "ÜRDÜN DİNARI")]
        JOD = 9,

        [Display(Name = "YENİ İSRAİL ŞEKELİ")]
        ILS = 10,

        [Display(Name = "MACAR FORİNTİ")]
        HUF = 11,

        [Display(Name = "EURO")]
        EUR = 12,

        [Display(Name = "ESKİ TÜRKMENİSTAN MANATI")]
        TMM = 13,

        [Display(Name = "IRAK DİNARI")]
        IQD = 14,

        [Display(Name = "LİBYA DİNARI")]
        LYD = 15,

        [Display(Name = "ÇEK KORUNASI")]
        CZK = 16,

        [Display(Name = "RUS RUBLESİ")]
        RUB = 17,

        [Display(Name = "ALMAN MARKI")]
        DEM = 18,

        [Display(Name = "BELÇİKA FRANGI")]
        BEF = 19,

        [Display(Name = "LÜKSEMBURG FRANGI")]
        LUF = 20,

        [Display(Name = "İSPANYOL PESETASI")]
        ESP = 21,

        [Display(Name = "FRANSIZ FRANGI")]
        FRF = 22,

        [Display(Name = "İNGİLİZ STERLİNİ")]
        GBP = 23,

        [Display(Name = "İRLANDA LİRASI")]
        IEP = 24,

        [Display(Name = "100 İTALYAN LİRETİ")]
        ITL = 25,

        [Display(Name = "HOLLANDA FLORİNİ")]
        NLG = 26,

        [Display(Name = "AVUSTURYA ŞİLİNİ")]
        ATS = 27,

        [Display(Name = "PORTEKİZ ESKÜDOSU")]
        PTE = 28,

        [Display(Name = "FİN MARKKASI")]
        FIM = 29,

        [Display(Name = "YUNAN DRAHMİSİ")]
        GRD = 30,

        [Display(Name = "TÜRKMENİSTAN MANATI")]
        TMT = 31,

        [Display(Name = "GÜRCİSTAN LARİSİ")]
        GEL = 32,

        [Display(Name = "KAZAKİSTAN TENGESİ")]
        KZT = 33,

        [Display(Name = "İSVİÇRE FRANGI")]
        CHF = 34,

        [Display(Name = "MAKEDONYA DİNARI")]
        MKD = 35,

        [Display(Name = "BOSNA HERSEK MARK")]
        BAM = 36,

        [Display(Name = "AZERBAYCAN MANATI")]
        AZN = 37,

        [Display(Name = "OZBEKİSTAN SUM")]
        UZS = 38,

        [Display(Name = "BİRLEŞİK ARAP EMİR. DİRHEMİ")]
        AED = 39,

        [Display(Name = "HIRVAT KUNASI")]
        HRK = 40,

        [Display(Name = "YENİ BULGAR LEVASI")]
        BGN = 41,

        [Display(Name = "100 JAPON YENİ")]
        JPY = 42,

        [Display(Name = "HİNDİSTAN RUPİSİ")]
        INR = 43,

        [Display(Name = "ÇİN YUANI RENMİNBİ")]
        CNY = 44,

        [Display(Name = "İSVEÇ KRONU")]
        SEK = 45,

        [Display(Name = "DANİMARKA KRONU")]
        DKK = 46,

        [Display(Name = "NORVEÇ KRONU")]
        NOK = 47,

        [Display(Name = "5 GR ZİRAAT ALTINI")]
        A05 = 48,

        [Display(Name = "10 GR ZİRAAT ALTINI")]
        A10 = 49,

        [Display(Name = "1 GR KÜLÇE ALTIN")]
        A01 = 50,

        [Display(Name = "ALTIN")]
        XAU = 51,

        [Display(Name = "MEVDUAT ALTIN")]
        A02 = 52,

        [Display(Name = "CEZAYIR DINARI")]
        DZD = 53,

        [Display(Name = "KATAR RİYALİ")]
        QAR = 54,

        [Display(Name = "UMMAN RİYALİ")]
        OMR = 55,

        [Display(Name = "TÜRK LİRASI")]
        TRY = 56,

        [Display(Name = "SUUDİ ARABİSTAN RİYALİ")]
        SAR = 57,

        [Display(Name = "BAHREYN DINARI")]
        BHD = 58,

        [Display(Name = "POTA DÖVİZ KODU")]
        SDR = 59,

        [Display(Name = "EVALUASYON")]
        EVA = 60,

        [Display(Name = "FAS PARA BİRİMİ (DİRHEM)")]
        MAD = 61,

        [Display(Name = "SLOVAK KORUNASI")]
        SKK = 62,

        [Display(Name = "GÜNEY AFRİKA RANDI")]
        ZAR = 63,

        [Display(Name = "POLONYA ZLOTİSİ")]
        PLN = 64,

        [Display(Name = "MISIR POUNDU")]
        EGP = 65,

        [Display(Name = "TUNUS DİNARI")]
        TND = 66,

        [Display(Name = "CIN OFFSHORE YUAN")]
        CNH = 67,

        [Display(Name = "RUMEN LEYI")]
        ROL = 68,

        [Display(Name = "AVRUPA PARA BİRİMİ")]
        ECU = 69,

        [Display(Name = "GÜMÜŞ")]
        XAG = 70,

        [Display(Name = "YENİ ZELANDA DOLARI")]
        NZD = 71,

        [Display(Name = "PLATİN (GR)")]
        PLT = 72,

        [Display(Name = "ALÜMİNYUM (TON)")]
        ALM = 73,

        [Display(Name = "Endonezya Rupıah")]
        IDR = 74,

        [Display(Name = "Meksika Pezosu")]
        MXN = 75,

        [Display(Name = "Nepal Rupisi")]
        NPR = 76,

        [Display(Name = "Brezilya Real")]
        BRL = 77,

        [Display(Name = "Tayland Bahtı")]
        THB = 78,

        [Display(Name = "Singapur Doları")]
        SGD = 79,

        [Display(Name = "Şili Pezosu")]
        CLP = 80,

        [Display(Name = "Hong Kong Doları")]
        HKD = 81,

        [Display(Name = "Güney Kore Wonu")]
        KRW = 82,

        [Display(Name = "Malezya Ringgiti")]
        MYR = 83,

        [Display(Name = "Yeni Tavyan Doları")]
        TWD = 84,
    }
}
