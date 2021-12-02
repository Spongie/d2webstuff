import { LoginResponse } from "../models/LoginResponse";
import HttpCommon from "../util/HttpCommon";

export class AuthService {
    isAuthenticated(): boolean {
        const sessionId = window.sessionStorage.getItem('sessionid');

        return sessionId != null;
    }

    async login(username: string, password: string): Promise<boolean> {
        const response = await HttpCommon.post<LoginResponse>("/Login", {
            Username: username,
            Password: password
        });

        if (response.isOk) {
            window.sessionStorage.setItem('sessionid', response.sessionId);
        }

        return response.isOk;
    }
}