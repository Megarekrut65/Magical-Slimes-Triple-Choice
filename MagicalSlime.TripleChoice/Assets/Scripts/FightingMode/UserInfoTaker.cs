using Global;

namespace FightingMode
{
    public static class UserInfoTaker
    {
        public static UserInfo Take()
        {
            UserInfo info = new UserInfo();
            info.name = DataSaver.LoadUsername();
            info.cups = FightingSaver.LoadCups();
            info.maxLevel = DataSaver.LoadMaxLevelForAccount();
            info.slimeName = DataSaver.LoadSlimeName();
            info.slimeType = DataSaver.LoadSlimeType();
            info.hat = DataSaver.LoadCurrentHat();

            return info;
        }
    }
}