
const apiBase = "/api";

const authController = apiBase + "/auth";
export const auth = {
    ping: authController + "/ping",
    request: authController + "/request",
    complete: authController + "/complete"
};

const userController = apiBase + "/user";
export const user = {
    inventory: userController
};

