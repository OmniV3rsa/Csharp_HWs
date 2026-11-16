Задача 1

а) В json попадут только Name и Score, потому что это публичные свойства (у них есть get и set).
secretCode не попадет, потому что оно private и это просто поле, а не свойство.
IsAlive тоже не попадет, так как у него нет { get; set; }, то есть это обычное поле, а JsonSerializer по умолчанию берет только публичные свойства.

б) Надо добавить атрибут [JsonInclude] перед secretCode (и подключить System.Text.Json.Serialization). Плюс если оставлять его именно полем, то при сериализации нужно еще включить настройку IncludeFields = true в JsonSerializerOptions (либо сделать его приватным свойством):

public class Player
{
    public string Name { get; set; }
    public int Score { get; set; }
    [JsonInclude]
    private int secretCode { get; set; } = 42;
    public bool IsAlive;
}

в) IsAlive не попадает, потому что это поле, а поля по умолчанию игнорируются.
1 способ: сделать его свойством, дописав { get; set; }:
public bool IsAlive { get; set; }
2 способ: передать в Serialize настройки с включенным IncludeFields = true (или тоже написать над ним [JsonInclude]):
JsonSerializerOptions options = new JsonSerializerOptions();
options.IncludeFields = true;
string json = JsonSerializer.Serialize(p, options);


Задача 2

а)
public class Game
{
    public string Title { get; set; }
    public int Year { get; set; }
    public double Rating { get; set; }
    public string[] Genres { get; set; }
    public bool IsMultiplayer { get; set; }
}

б) По умолчанию при десериализации учитывается регистр букв. В json написано title с маленькой буквы, а в классе Title с большой, поэтому он не сопоставил их и оставил в Title значение null.
Чтобы все работало без смены имен в классе, нужно отключить проверку регистра в настройках вот так:

JsonSerializerOptions options = new JsonSerializerOptions();
options.PropertyNameCaseInsensitive = true;
Game g = JsonSerializer.Deserialize<Game>(json, options);

в) Да, это валидный json, в массиве json могут лежать элементы разных типов. В C# это можно десериализовать в массив или список.


Задача 3

1. File (File.ReadAllText) - файл маленький, всего 10 строк, проще прочитать его целиком одной строчкой кода.
2. File (File.WriteAllText) - сохранение весит немного и при автосохранении надо просто полностью перезаписать файл новой строкой json.
3. FileStream - лог может вырасти до нескольких гигабайт, через File он бы грузился в память целиком, а через поток можно просто дописывать новые строки в конец файла.
4. File (File.Exists и File.ReadAllText) - тут нужно просто проверить наличие файла и сразу прочитать его в строку, в классе File для этого уже есть готовые методы.
5. FileStream - потому что нужно заменить всего 10 байт в середине большого файла без его полной перезаписи, а в FileStream можно передвинуть курсор на нужную позицию.