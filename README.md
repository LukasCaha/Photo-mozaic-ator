# Specifikace Photo-mozaic-ator pro NPRG035

Hlavní funkce tohoto programu bude převést uživatelský obrázek do mozaiky složené z jiných obrázků. Je více než důležité aby cílová mozaika byla co nejvěrnější napodobeninou původního obrázku. Zároveň ale může program obětovat přesnost napodobení pro zvýšení rychlosti převodu.

Algoritmus na převod poběží v jazyce C# na pozadí windowsové formulářové aplikace.

## Názvosloví

Vzor = část původního obrázku pevné velikosti

Kamínek = zmenšený obrázek ze sady, tvoří cílovou mozaiku

Sada = soubor obrázků (kamínky/obrázky pro tvorbu kamínků)

## Funkce programu

### Barevné metriky

Pro vhodnou reprezentaci vzoru kamínkem bude potřeba určit, který kamínek je nejvěrnější pro daný vzor. Toho docílíme použitím vhodné metriky. Ta určí vzdálenost |kamínek, vzor| a následně vybere kamínek s nejmenší vzdáleností.

Výkon metriky pro daný problém lze určit jen velmi těžko, pro různé sady a vstupní obrázky může produkovat kvalitativně rozdílné výstupy. Proto implementuji více metrik a následně vyhodnotím, která je nejvhodnější.

1. Bitový rozdíl barvy #aaaaaa s barvou #bbbbbb
2. Čtvercová vzdálenost barvy #aaaaaa s barvou #bbbbbb
3. Knihovna s CIE metrikou

### Algoritmické funkce

#### Tvorba kamínků

Pro tvorbu mozaiky jsou potřeba kamínky, které budou napodobovat části původního obrázku. Kamínek musí co nejvěrněji napodobit jeho vzor, aby byl obsah původního obrázku rozpoznatelný i z mozaiky.

Uživatel, nebo autor programu předá programu složku (sadu) s velkým množstvím obrázků. Na množinu předaných obrázků je několik netriviálních požadavků:

1. Celkové spektrum barevných palet obrázků pokrývá co nejvěrněji RGB prostor
2. Velikost sady je řádově úměrná počtu potřebných kamínků

##### Možné zdroje sad obrázků
* Obličeje ze stránky https://thispersondoesnotexist.com/
* Jednobarevné čtverce (pixelizace obrázku)
* Sada poskytnutá uživatelem

##### Měnitelné parametry

* Velikost kamínku v pixelech, je možné mít i obdélníkový kamínek
* Výběr rozlišení barevné palety
  * každý kamínek bude reprezentovat jednu RGB barvu podle metriky
  * rozlišení palety = číslo xxx xxx/16 777 216 které určuje kolika kamínků bude potřeba, pokud nebude nalezen kamínek v daném rozmezí, vybere se nejbližší
* Výběr zda bude pro jednu barvu palety více možných kamínků
  * Pro barvu #xxxxxx bude použit **první** obrázek ze sady, který vyhovuje kritériům
  * Pro barvu #xxxxxx bude použit **poslední** obrázek ze sady, který vyhovuje kritériům
  * Pro barvu #xxxxxx bude použit obrázek ze sady, který **nejlépe** vyhovuje kritériům (je nejblíž dané barvě dle metriky)
  * Pro barvu #xxxxxx budou použity **všechny** obrázky ze sady, který vyhovují kritériům (náhodně střídané)

#### Stavba mozaiky

Sestavení cílové mozaiky bude probíhat podle následujícího postupu:

1. Uživatel vloží obrázek (#1)
2. Uživatel vybere sadu kamínků
3. Uživatel zvolí kolik (#2) kamínků se použije na reprodukci obrázku #1
4. Program rozdělí vložený obrázek na #2 stejných vzorů
5. Program ke každému vzoru najde pomocí metriky nejbližší kamínek
6. Program nahradí každý vzor nějakým kamínkem a ty následně poskládá do cílové mozaiky

##### Měnitelné parametry

* Výstupní velikost obrázku
  * Obrázek bude zachovávat poměr stran
  * Obrázek bude volně natažený podle vstupních rozměrů a požadovaných výstupních
  * Způsob zacházení se souřadnicovým koncem obrázku
    * Přidat neexistující pixely na doplnění poslední řady kamínků
    * Na začátku ořezat obrázek, aby seděl s rozměrem kamínků
* Výběr sady kamínků
* Možnost uložit mozaiku
* Možnost přidat *před/po* porovnání

### Funkce uživatelského rozhraní

Aby bylo pro uživatele převedení obrázku na mozaiku co nejjednodušší, bude každý zmíněný parametr mít defaultní hodnotu. Jediné co bude muset uživatel poskytnout bude konvertovaný obrázek. Přenastavování parametrů bude dostupně v okně "Advanced options".

Při započetí konverze obrázku na mozaiku bude mít uživatel k dispozici přesnou načítací lištu a hotový obrázek se mu ihned po dokončení objeví. Uživatel bude mít následně možnost obrázek uložit a konvertovat další.



------



###### Poznámky pod čarou

Tyto poznámky slouží pro mě, abych nezapomněl všechny související informace o projektu.

1. Ve WFA bude nutné použít vícevláknového workera, aby mohl fungovat loading bar ad.
2. Funkce pro práci s BMP SetPixel a GetPixel jsou pomalé pro mé použití a bude vhodné najít rychlejší variantu.
3. Loading může vypadat jako objevující se mozaika (Cinebench) nebo jako transition mezi původním obrázkem a rozpixelovanou verzí téhož obrázku.
4. Práce s hodně pixely bude pomalá a tak se bude hodit mít předpočítané všechny sady, také vyhledávání barvy v sadě musí probíhat přes nějakou adaptaci binárního vyhledávání na barevné prostory. Alternativně si může generátor mozaiky říkat přímo o konkrétní barvu v O(1) a sada kamínků si otestuje zda obsahuje všechny barvy ve slíbené paletě.
5. Sady kamínků na disku budou zabírat nezanedbatelné místo, pro plnou barevnou paletu bude velikost 16 milionů souborů což je moc.
6. Generovaný obrázek může být větší než původní, tímto je možné "zvyšovat" rozlišení. Reálně to bude fungovat tak že uživatel bude vidět detail na kamíncích a bude to vhodné pro tisk, kde může být detail vidět.







# Prototyp aplikace

## 27.8. 2020

Pro aplikaci jsem sestrojil prototyp, abych si odzkoušel různé mechaniky. Příklad výstupního obrázku http://lukascaha.com/example.png

Je patrný nedostatek s opakujícími se obličeji, hlavně v extrémních případech jako je světlá/tmavá místa. Detailní plochy vypadají rozumně.

Konverze 1920x1080 trvala na mém PC 8s. Implementace není optimální, ale výkon PC je vyšší než průměr. Délka generování sady kamínků je také podstatně vyšší, případně při prvním setupu je nutné buď poslat s programem sady nebo je nechat vygenerovat. Sada 10000 obličejů (1024x1024px) se mi skriptem stahovala 3h a zabrala 5GB. Po redukci na paletu se kterou byla vytvořena tato mozaika je složka veliká <10MB.

## 4.3. 2021

Aplikace je nyní v WFA. Je implementovaná funkcionalita tvorby kamínků a následné generování mozaik.

![image-20210304233923828](C:\Users\Lukas\AppData\Roaming\Typora\typora-user-images\image-20210304233923828.png)

### Příklady

#### Předtím 
![Penguins](C:\Users\Public\Pictures\Sample Pictures\Penguins.jpg)
#### Potom
![B06C0](C:\Users\Lukas\source\repos\Photo-mozaic-ator\Photo-mozaic-ator\working_dir\B06C0.png)

------

#### Předtím 

![Koala](C:\Users\Public\Pictures\Sample Pictures\Koala.jpg)

#### Potom

![67AF5](C:\Users\Lukas\source\repos\Photo-mozaic-ator\Photo-mozaic-ator\working_dir\67AF5.png)
