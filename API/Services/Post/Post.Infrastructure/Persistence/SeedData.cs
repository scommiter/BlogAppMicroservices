using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Post.Infrastructure.Persistence
{
    public class Seed
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        public Seed(ILogger logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            if (!_context.Posts.Any())
            {
                await _context.Posts.AddRangeAsync(
                    new Domain.Entities.Post
                    {
                        Id = Guid.NewGuid(),
                        Title = "What Is Happiness?",
                        UserName = "Lupin",
                        Content = "Happiness is something that people seek to find, yet what defines happiness can vary from one person to the next. Typically, happiness is an emotional state characterized by feelings of joy, satisfaction, contentment, and fulfillment. While happiness has many different definitions, it is often described as involving positive emotions and life satisfaction. When most people talk about the true meaning of happiness, they might be talking about how they feel in the present moment or referring to a more general sense of how they feel about life overall. Because happiness tends to be such a broadly defined term, psychologists and other social scientists typically use the term 'subjective well-being' when they talk about this emotional state. Just as it sounds, subjective well-being tends to focus on an individual's overall personal feelings about their life in the present. Two key components of happiness (or subjective well-being) are: The balance of emotions: Everyone experiences both positive and negative emotions, feelings, and moods. Happiness is generally linked to experiencing more positive feelings than negative ones. Life satisfaction: This relates to how satisfied you feel with different areas of your life including your relationships, work, achievements, and other things that you consider important. Another definition of happiness comes from the ancient philosopher Aristotle, who suggested that happiness is the one human desire, and all other human desires exist as a way to obtain happiness. He believed that there were four levels of happiness: happiness from immediate gratification, from comparison and achievement, from making positive contributions, and from achieving fulfillment."
                    },
                    new Domain.Entities.Post
                    {
                        Id = Guid.NewGuid(),
                        Title = "A traveler's guide to Tet holiday",
                        UserName = "Jack",
                        Content = "Lunar New Year or Tết Nguyên Đán, is Vietnam’s most significant celebration. Across Vietnam, during this time families reunite and honour their ancestors, while praying for luck, prosperity and health in the new year. The public holiday may only run for one week, but in reality, Tết celebrations last much longer. If you’re visiting Vietnam around Lunar New Year, here’s what you can expect. Tết marks the first day of the Lunar New Year, and the beginning of spring in the North of Vietnam. The holiday traces back to the early days of Vietnamese settlements in the Red River Delta, when Tết meant a new cycle of wet rice cultivation. Today, the meaning of the holiday runs much deeper than its farming roots: Vietnamese culture emphasises the importance of a fortuitous fresh start, surrounded by family and loved ones."
                    },
                    new Domain.Entities.Post
                    {
                        Id = Guid.NewGuid(),
                        Title = "How to Know When You Love Someone",
                        UserName = "Alex",
                        Content = "Love is a set of emotions and behaviors characterized by intimacy, passion, and commitment. It involves care, closeness, protectiveness, attraction, affection, and trust. Love can vary in intensity and can change over time. It is associated with a range of positive emotions, including happiness, excitement, life satisfaction, and euphoria, but it can also result in negative emotions such as jealousy and stress.1 When it comes to love, some people would say it is one of the most important human emotions. Yet despite being one of the most studied behaviors, it is still the least understood. For example, researchers debate whether love is a biological or cultural phenomenon.2 Love is most likely influenced by both biology and culture. Although hormones and biology are important, the way we express and experience love is also influenced by our personal conceptions of love."
                    },
                    new Domain.Entities.Post
                    {
                        Id = Guid.NewGuid(),
                        Title = "Married Life",
                        UserName = "Rose",
                        Content = "Marriage is a vocation to holiness. From their first days as husband and wife through their golden years, married couples have the awesome task of witnessing to God’s faithful love to each other, their children, and society. No couple does this perfectly, and everyone needs help when love feels strained and the going gets tough. All marriages can grow in knowledge, faith, joy, and love. Whether you are just starting out in marriage or have fifty years under your belt, the advice, real life stories, and Church teaching here can help strengthen and bless your marriage. For a newly engaged couple, learning Natural Family Planning (NFP) is informative, interesting, at times a little embarrassing, but always enlightening. Living NFP, on the other hand, is a different story. It is a story about connections, unique and fulfilling. It involves the use of information that we then apply to the reality of everyday married life. At the beginning of our married life, we used NFP to avoid pregnancy, as the time was not right for it. Currently, we are using NFP to achieve pregnancy. We were delighted to find that the two focuses of NFP have made our young marriage both more focused and more intimate. Unlike contraception, which usually places full burden of family planning on the woman, NFP promotes shared responsibility of the fertility of both the husband and wife. It lends a spirit of togetherness to a marriage. There’s no “Have you taken your pill?” That is, “Are you safe?” In our marriage there’s no holding back that precious part of ourselves–our fertility. Rather than a burden to be dealt with, for us it is a blessing to be understood and respected. The complete self-giving says, “I love all of you.” The benefits of NFP extend beyond family planning. We’d heard that often the husband will develop a deeper respect for his wife and the gift of her fertility. In practice, we’ve found this to be true. A constant awareness of cycles and phases makes it easier to perceive when to be loving and gentle, extra patient and thoughtful, and when to resume physical intimacy. Unlike a couple using contraception in their marriage, sex is not always an option for two who are living NFP. That’s a good thing, contrary to what popular culture might imply. By experiencing times when we cannot engage in physical intimacy, the moments that we can are made all the more poignant and precious. Even when we want to engage, and the chart says “no way, buddy,” it lends an element of bittersweet waiting. After all, consider the alternative: When a woman is on the pill or using some other kind of chemical contraceptive, she’s always available for sex. There’s no waiting, no longing, just indulging whenever you want. Nice at first, perhaps, but over time spontaneity and passion fade all the more quickly by the frequency of the intimacy. Oftentimes sexual intimacy will becomes less mutual over time in a contracepting marriage and more mandatory, and thus less rewarding for one or both spouses. Periodic abstinence in our marriage has opened up broader channels of communication between us. Like many young couples, we both are currently employed. Commuting, daily exercising, paying bills, preparing dinner, outside commitments . . . all are busy but necessary activities in a healthy lifestyle, but collectively tiresome as well. Tired couples find it difficult to talk in the evenings, and would prefer to veg out. We’re no different."
                    }
                    );
            }
        }
        //public static async Task SeedPostAsync(DataContext context)
        //{
        //    if (!await context.Posts.AnyAsync())
        //    {
        //        var posts = new List<Domain.Entities.Post>
        //        {
                    //new Domain.Entities.Post
                    //{
                    //    Id = Guid.NewGuid(),
                    //    Title = "What Is Happiness?",
                    //    Content = "Happiness is something that people seek to find, yet what defines happiness can vary from one person to the next. Typically, happiness is an emotional state characterized by feelings of joy, satisfaction, contentment, and fulfillment. While happiness has many different definitions, it is often described as involving positive emotions and life satisfaction. When most people talk about the true meaning of happiness, they might be talking about how they feel in the present moment or referring to a more general sense of how they feel about life overall. Because happiness tends to be such a broadly defined term, psychologists and other social scientists typically use the term 'subjective well-being' when they talk about this emotional state. Just as it sounds, subjective well-being tends to focus on an individual's overall personal feelings about their life in the present. Two key components of happiness (or subjective well-being) are: The balance of emotions: Everyone experiences both positive and negative emotions, feelings, and moods. Happiness is generally linked to experiencing more positive feelings than negative ones. Life satisfaction: This relates to how satisfied you feel with different areas of your life including your relationships, work, achievements, and other things that you consider important. Another definition of happiness comes from the ancient philosopher Aristotle, who suggested that happiness is the one human desire, and all other human desires exist as a way to obtain happiness. He believed that there were four levels of happiness: happiness from immediate gratification, from comparison and achievement, from making positive contributions, and from achieving fulfillment."
                    //},
                    //new Domain.Entities.Post
                    //{
                    //    Id = Guid.NewGuid(),
                    //    Title = "A traveler's guide to Tet holiday",
                    //    Content = "Lunar New Year or Tết Nguyên Đán, is Vietnam’s most significant celebration. Across Vietnam, during this time families reunite and honour their ancestors, while praying for luck, prosperity and health in the new year. The public holiday may only run for one week, but in reality, Tết celebrations last much longer. If you’re visiting Vietnam around Lunar New Year, here’s what you can expect. Tết marks the first day of the Lunar New Year, and the beginning of spring in the North of Vietnam. The holiday traces back to the early days of Vietnamese settlements in the Red River Delta, when Tết meant a new cycle of wet rice cultivation. Today, the meaning of the holiday runs much deeper than its farming roots: Vietnamese culture emphasises the importance of a fortuitous fresh start, surrounded by family and loved ones."
                    //},
                    //new Domain.Entities.Post
                    //{
                    //    Id = Guid.NewGuid(),
                    //    Title = "How to Know When You Love Someone",
                    //    Content = "Love is a set of emotions and behaviors characterized by intimacy, passion, and commitment. It involves care, closeness, protectiveness, attraction, affection, and trust. Love can vary in intensity and can change over time. It is associated with a range of positive emotions, including happiness, excitement, life satisfaction, and euphoria, but it can also result in negative emotions such as jealousy and stress.1 When it comes to love, some people would say it is one of the most important human emotions. Yet despite being one of the most studied behaviors, it is still the least understood. For example, researchers debate whether love is a biological or cultural phenomenon.2 Love is most likely influenced by both biology and culture. Although hormones and biology are important, the way we express and experience love is also influenced by our personal conceptions of love."
                    //},
                    //new Domain.Entities.Post
                    //{
                    //    Id = Guid.NewGuid(),
                    //    Title = "Married Life",
                    //    Content = "Marriage is a vocation to holiness. From their first days as husband and wife through their golden years, married couples have the awesome task of witnessing to God’s faithful love to each other, their children, and society. No couple does this perfectly, and everyone needs help when love feels strained and the going gets tough. All marriages can grow in knowledge, faith, joy, and love. Whether you are just starting out in marriage or have fifty years under your belt, the advice, real life stories, and Church teaching here can help strengthen and bless your marriage. For a newly engaged couple, learning Natural Family Planning (NFP) is informative, interesting, at times a little embarrassing, but always enlightening. Living NFP, on the other hand, is a different story. It is a story about connections, unique and fulfilling. It involves the use of information that we then apply to the reality of everyday married life. At the beginning of our married life, we used NFP to avoid pregnancy, as the time was not right for it. Currently, we are using NFP to achieve pregnancy. We were delighted to find that the two focuses of NFP have made our young marriage both more focused and more intimate. Unlike contraception, which usually places full burden of family planning on the woman, NFP promotes shared responsibility of the fertility of both the husband and wife. It lends a spirit of togetherness to a marriage. There’s no “Have you taken your pill?” That is, “Are you safe?” In our marriage there’s no holding back that precious part of ourselves–our fertility. Rather than a burden to be dealt with, for us it is a blessing to be understood and respected. The complete self-giving says, “I love all of you.” The benefits of NFP extend beyond family planning. We’d heard that often the husband will develop a deeper respect for his wife and the gift of her fertility. In practice, we’ve found this to be true. A constant awareness of cycles and phases makes it easier to perceive when to be loving and gentle, extra patient and thoughtful, and when to resume physical intimacy. Unlike a couple using contraception in their marriage, sex is not always an option for two who are living NFP. That’s a good thing, contrary to what popular culture might imply. By experiencing times when we cannot engage in physical intimacy, the moments that we can are made all the more poignant and precious. Even when we want to engage, and the chart says “no way, buddy,” it lends an element of bittersweet waiting. After all, consider the alternative: When a woman is on the pill or using some other kind of chemical contraceptive, she’s always available for sex. There’s no waiting, no longing, just indulging whenever you want. Nice at first, perhaps, but over time spontaneity and passion fade all the more quickly by the frequency of the intimacy. Oftentimes sexual intimacy will becomes less mutual over time in a contracepting marriage and more mandatory, and thus less rewarding for one or both spouses. Periodic abstinence in our marriage has opened up broader channels of communication between us. Like many young couples, we both are currently employed. Commuting, daily exercising, paying bills, preparing dinner, outside commitments . . . all are busy but necessary activities in a healthy lifestyle, but collectively tiresome as well. Tired couples find it difficult to talk in the evenings, and would prefer to veg out. We’re no different."
                    //}
        //        };
        //        context.Posts.AddRange(posts);
        //        await context.SaveChangesAsync();
        //    }
        //}
    }
}
