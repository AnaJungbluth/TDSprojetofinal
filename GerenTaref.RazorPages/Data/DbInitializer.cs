namespace GerenTaref.RazorPages.Data {
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            // if(!context.Events!.Any()) {
            //     var events = new EventModel[] {
            //         new EventModel {
            //             Name = "Torneio de Truco",
            //             Description = "Campeonato acadêmico de CC da UTFPR",
            //             Date = DateTime.Parse("2023-04-03")
            //         }
            //     };
            //     context.AddRange(events);
            // }

            // if (!context.Players!.Any()) {
            //     var players = new PlayerModel[] {
            //         new PlayerModel {
            //             Name = "Pablo",
            //             Age = 50
            //         }
            //     };
            //     context.AddRange(players);
            // }

            context.SaveChanges();
        }
    }
}