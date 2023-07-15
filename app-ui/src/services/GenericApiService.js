import axios from 'axios';

const baseURL = "https://localhost:7246"

class GenericApiService {

    async post(path, data) {
        try {
            const response = await axios.post(`${baseURL}${path}`, data);
            if (path === '/Auth/Login' && response.status === 200) {
                localStorage.setItem('token', JSON.stringify(response.data.replace(/^"(.*)"$/, "$1")));
            }
            return response;
        } catch (error) {
            return error;
        }
    }

    async get(path) {
        try {
            const token = localStorage.getItem("token");
            const headers = {
                Authorization: `bearer ${token.replace(/^"(.*)"$/, "$1")}`,
            };

            const response = await axios.get(`${baseURL}${path}`, { headers });
            return response;
        } catch (error) {
            return error;
        }
    }
}

export default GenericApiService;