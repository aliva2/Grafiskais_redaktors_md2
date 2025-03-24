# Grafiskais_redaktors_md2

Šī programma ir grafiskais redaktors, kas izveidota, izmantojot C# Windows Forms. Lietotne ļauj lietotājiem zīmēt dažādas formas, mainīt krāsas, rīkus un saglabāt vai izdrukāt savus zīmējumus.

## Funkcijas

- **Izvēlne**:
    - **New** (Jauns): Notīra audeklu un atiestata visu.
    - **Open** (Atvērt): Atver esošu zīmējumu failu.
    - **Save** (Saglabāt): Saglabā pašreizējo zīmējumu.
    - **Print** (Izdrukāt): Izdrukā pašreizējo zīmējumu.
    
    Katram izvēlnes vienumam jāizpilda **skaņas efekts**, kad tas tiek izvēlēts.

- **Rīkjosla**:
    - Rīkjoslā jāizvēlas no dažādiem zīmēšanas rīkiem:
        - **Pen** (Pildspalva)
        - **Line** (Līnija)
        - **Rectangle** (Taisnstūris)
        - **Ellipse** (Elipse)
        - **Eraser** (Dzēšgumija)
        
    Katram rīkam jāizveido ikona.

- **Krāsu izvēles dialogs**:
    - Krāsu izvēles dialogs (ColorDialog) jāizmanto, lai izvēlētos krāsu zīmēšanas rīkiem.
    - Objektiem jāzīmējas ar izvēlēto krāsu.

## Uzdevumi

1. **Izvēlnes īstenošana**:
    - Izvēlnei jābūt šādām komandām:
      - `New` (Jauns)
      - `Open` (Atvērt)
      - `Save` (Saglabāt)
      - `Print` (Izdrukāt)
    - Katram izvēlnes vienumam jābūt skaņas efektam.

2. **Rīkjosla**:
    - Rīkjoslā jābūt izvēlei no šādiem zīmēšanas rīkiem:
        - **Pen** (Pildspalva) - brīvās rokas zīmēšana
        - **Line** (Līnija) - taisnu līniju zīmēšana
        - **Rectangle** (Taisnstūris) - taisnstūra zīmēšana
        - **Ellipse** (Elipse) - elipses zīmēšana
        - **Eraser** (Dzēšgumija) - zīmēšanas dzēšana
    - Katram rīkam jābūt saistītai ikonai uz rīkjoslas.

3. **Krāsu izvēle**:
    - Izmantot krāsu izvēles dialogu (ColorDialog), lai izvēlētos krāsu zīmēšanas rīkiem.
    - Kad lietotājs izvēlas krāsu, visi zīmētie objekti jāizveido ar izvēlēto krāsu.

4. **Skaņas efekti**:
    - Katrs izvēlnes komanda (`New`, `Open`, `Save`, `Print`) jāatskaņo skaņas efekts, kad tas tiek izvēlēts.

## Lietošana

1. Atveriet programmu, lai redzētu zīmēšanas audeklu un rīkjoslu.
2. Izvēlieties rīku no rīkjoslas (Pildspalva, Līnija, Taisnstūris, Elipse vai Dzēšgumija).
3. Izvēlieties krāsu, izmantojot `ColorDialog` pogu.
4. Zīmējiet uz balta audekla, izmantojot izvēlēto rīku un krāsu.
5. Izmantojiet izvēlni, lai saglabātu, atvērtu vai izdrukātu savu zīmējumu.
6. Baudiet skaņas efektus, izmantojot izvēlnes vienumus!

## Izstrāde

- Visual Studio (vai jebkura saderīga IDE C# izstrādei)
- .NET Framework (Windows Forms lietotnēm)
