namespace T2WebSoket.Services
{
    public class ChatBotService
    {
        public string ProcessMessage(string message)
        {
            if (message.Contains("что умеет этот бот"))
            {
                return "Я могу отвечать на вопросы и помогать с информацией.";
            }
            else if (message.Contains("поддержка"))
            {
                return "Для поддержки обратитесь в службу поддержки.";
            }
            return "Неизвестная команда.";
        }
    }
}
