namespace HerbShop.Utils
{
    public class Routing
    {
        public static string Layout => "~/Views/Shared/_Layout.cshtml";
        public static string EShopIndex => "/";
        public static string AccountIndex => "/account";
        public static string AccountLogin => "/account/login";
        public static string AccountRegister => "/account/register";
        public static string AccountData => "/account/data";
        public static string AccountLogout => "/account/logout";
        public static string CartIndex => "/cart";
        public static string CartAdd => "/cart/add";
        public static string CartOrder => "/cart/order";

        public static string WithParameters(string route, params (string name, string value)[] parameters)
        {
            route += "?";
            foreach (var parameter in parameters)
            {
                route += $"{parameter.name}={parameter.value}&";
            }
            route.Remove(route.Length - 1);
            return route;
        }
    }
}
