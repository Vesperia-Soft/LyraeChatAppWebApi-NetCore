import axios from 'axios';

const baseURL = "https://localhost:7246"

class GenericApiService {

    async post(path, data) {
        try {
            const response = await axios.post(`${baseURL}${path}`, data);
            if (path === '/Auth/Login' && response.status === 200) {
                localStorage.setItem('token', JSON.stringify(response.data));
            }
            return response;
        } catch (error) {
            return error;
        }
    }
}

export default GenericApiService;