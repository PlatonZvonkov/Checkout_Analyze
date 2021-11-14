namespace CheckoutAnalyze.Extractors
{
    class RegexPaths
    {
         protected private const string INN_REGEX = @"([И][Н][Н]\s\d{10})";
         protected private const string DEALER_REGEX = @"([П][о][с][т][а][в][щ][и][к][:punct:]\s\D*,)";
         protected private const string PRICE_BIG_REGEX = @"(Цена\s(\S*)\s*|Цена\s*)(\d*,\d*.\d*\s|\d*\s\d{3},\d{2}|\d*,\d{2}|\d*\w\d*\s)";
         protected private const string NAME_REGEX_OPTION1 =@"(Товары \Wработы, услуги\W\s*|Наименование\s*)(\S*\s\S*\s\S*\s\S*|\S*\s\S*\s\S*|\S*\s\S*)";
         protected private const string COUNT_VALUE_REGEX = @"(Кол-во\s*|Количество\s*)(\S*)";
         protected private const string COUNT_MEASURE_REGEX = @"(Ед.\nизм.\s*|Ед.\sизм.\s*|Ед.\s*)(\S*)";
         protected private const string NUMBER_SYMBOL = @"№";         
    }
}