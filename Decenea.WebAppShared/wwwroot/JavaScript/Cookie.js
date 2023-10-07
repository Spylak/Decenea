window.Methods = {
    SetCookie: function (cname, cvalue, exdays) {
        try {
            const d = new Date();
            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
            let expires = "expires=" + d.toUTCString();
            document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
            return "Success";
        } catch (error) {
            return error;
        }
    },

    GetCookie: function (cname) {
        try {

            let name = cname + "=";
            let ca = document.cookie.split(';');
            for (let i = 0; i < ca.length; i++) {
                let c = ca[i];
                while (c.charAt(0) == ' ') {
                    c = c.substring(1);
                }
                if (c.indexOf(name) == 0) {
                    return c.substring(name.length, c.length);
                }
            }
            return "";

        } catch (e) {
            return e;
        }
    },

    DeleteCookie: function (cname) {
        try {
            const d = new Date();
            d.setTime(d.getTime() + (-1 * 24 * 60 * 60 * 1000));
            let expires = "expires=" + d.toUTCString();
            document.cookie = cname + "=" + ";" + expires + ";path=/";
            return "Success";
        } catch (error) {
            return error;
        }
    }
}