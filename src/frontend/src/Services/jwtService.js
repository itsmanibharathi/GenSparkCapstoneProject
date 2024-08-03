import log from '../utility/loglevel.js';

class tokenService {
    constructor(moduleName) {
        this.moduleName = moduleName;
    }
    get = () => {
        log.debug('Token retrieved');
        return localStorage.getItem(`${this.moduleName}_token`);
    }

    select = (key) => {
        const token = this.get();
        if (!token) {
            return null;
        }
        const payload = token.split('.')[1];
        const decoded = atob(payload);
        const user = JSON.parse(decoded);
        return user[key] ?? null;
    }

    set = (token) => {
        localStorage.setItem(`${this.moduleName}_token`, token);
        log.debug(`Token set: ${token}`);

    }

    remove = () => {
        localStorage.removeItem(`${this.moduleName}_token`);
        log.debug('Token removed');
    }

    exists = () => {
        return localStorage.getItem(`${this.moduleName}_token`) ? true : false;
    }

}

export default tokenService;