using Global;

namespace FightingMode
{
    public static class UserInfoTaker
    {
        public static UserInfo Take()
        {
            UserInfo info = new UserInfo
            {
                name = DataSaver.LoadUsername(),
                cups = FightingSaver.LoadCups(),
                maxLevel = DataSaver.LoadMaxLevelForAccount(),
                slimeName = DataSaver.LoadSlimeName(),
                slimeType = DataSaver.LoadSlimeType(),
                hat = DataSaver.LoadCurrentHat()
            };

            return info;
        }
    }
}