using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WordCardsApi.Infrastructure.Settings;
using WordCardsApi.Models;

namespace WordCardsApi.Infrastructure.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly IPasswordHasher<User> _hasher;

    public MongoDbContext(IOptions<MongoDbSettings> mongoDbSettings, IPasswordHasher<User> hasher)
    {
        var client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        _database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);

        var indexModel = new CreateIndexModel<User>(Builders<User>.IndexKeys.Descending(u => u.Email),
            new CreateIndexOptions { Unique = true });
        Users.Indexes.CreateOne(indexModel);

        CreateSeedInformation();

    }

    private void CreateSeedInformation()
    {

        if(Users.CountDocuments(u => true) > 0)
        {
            return;
        }
        var user = new User
        {
            Id = "1",
            Name = "test user",
            Email = "test@gmail.com",
            HashedPassword = "12345"
        };

        user.HashedPassword = _hasher.HashPassword(user, user.HashedPassword);
        Users.InsertOne(user);
        var userWords = new List<UserWord>();
        string[] pair = "I - я, You - ты или вы, He - он, She - она, It - оно или это, We - мы, They - они, Me - мне или меня, Him - его или ему, Her - ее, Us - нас или нам, Them - их или им, My - мой или моя, Your - твой или ваш, His - его, Our - наш, Their - их, Who - кто, What - что или какой, Person - человек, People - люди, Man - мужчина, Woman - женщина, Child - ребенок, Boy - мальчик, Girl - девочка, Friend - друг, Family - семья, Name - имя, Be - быть, Have - иметь, Do - делать, Say - сказать, Go - идти или ехать, Get - получать или становиться, Make - делать или создавать, Know - знать, Think - думать, Take - брать, See - видеть, Come - приходить, Want - хотеть, Look - смотреть, Use - использовать, Find - находить, Give - давать, Tell - рассказывать, Work - работать, Call - звонить или называть, Try - пытаться, Ask - спрашивать, Need - нуждаться, Feel - чувствовать, Become - становиться, Leave - покидать или уходить, Put - класть или ставить, Mean - иметь в виду или значит, Keep - держать или сохранять, Let - позволять, Begin - начинать, Seem - казаться, Help - помогать, Talk - разговаривать, Turn - поворачивать, Start - начинать, Show - показывать, Hear - слышать, Play - играть, Run - бежать, Move - двигаться, Live - жить, Believe - верить, Bring - приносить, Happen - случаться, Write - писать, Sit - сидеть, Stand - стоять, Lose - терять, Pay - платить, Meet - встречаться, Learn - учиться, Change - менять, Lead - вести или лидировать, Understand - понимать, Watch - смотреть или наблюдать, Follow - следовать, Stop - останавливать, Create - создавать, Speak - говорить, Read - читать, Allow - позволять, Spend - тратить, Time - время, Year - год, Day - день, Week - неделя, Month - месяц, Way - путь или способ, Thing - вещь или предмет, World - мир, Life - жизнь, Hand - рука, Part - часть, Place - место, Case - случай или дело, Government - правительство, Company - компания, Number - число или номер, Group - группа, Problem - проблема, Fact - факт, Eye - глаз, Water - вода, Room - комната, Mother - мать, Father - отец, Area - область или район, Money - деньги, Story - история, Lot - много, Right - право или правая сторона, Study - учеба или исследование, Book - книга, Business - бизнес или дело, Issue - вопрос или проблема, Side - сторона, Kind - вид или тип, Head - голова, House - дом, Service - услуга или служба, Power - сила или власть, Hour - час, Line - линия или строка, End - конец, Game - игра, City - город, Community - сообщество, Good - хороший, New - новый, First - первый, Last - последний, Long - длинный, Great - отличный или великий, Little - маленький, Own - собственный, Other - другой, Old - старый, Big - большой, High - высокий, Different - разный или отличный, Small - маленький, Large - крупный или большой, Next - следующий, Early - ранний, Young - молодой, Important - важный, Few - немногие, Public - публичный или общественный, Bad - плохой, Same - тот же самый, Able - способный, Not - не, Also - также, More - больше, Only - только, Very - очень, Often - часто, Always - всегда, Never - никогда, Well - хорошо, Here - здесь, There - там, When - когда, Why - почему, How - как, Where - где, Again - снова, Together - вместе, Already - уже, Quick - быстро, Now - сейчас, Then - тогда, Out - снаружи или вон, Up - вверх, Down - вниз, About - о или около, Before - до или перед, After - после, Because - потому что, If - если, Or - или, But - но, And - и, With - с, Inside - внутри, Outside - снаружи, Between - между, Under - под".Split(", ");
        string[] words = pair.Select(p => p.Split(" - ")[0]).ToArray();
        string[] translations = pair.Select(p => p.Split(" - ")[1]).ToArray();

        for(int i = 0; i <= 99; i++)
        {
            var w = new UserWord()
            {
                UserId = user.Id,
                Word = words[i],
                Translation = translations[i],
                Language = "english",
            };
            userWords.Add(w);
        }

        UserWords.InsertMany(userWords);
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("users");
    public IMongoCollection<UserWord> UserWords => _database.GetCollection<UserWord>("userWords");
    public IMongoCollection<LearningSession> LearningSessions => _database.GetCollection<LearningSession>("learningSessions");
    public IMongoCollection<RefreshToken> RefreshTokens => _database.GetCollection<RefreshToken>("refreshTokens");
    public IMongoCollection<SessionWord> SessionWords => _database.GetCollection<SessionWord>("sessionWords");
}