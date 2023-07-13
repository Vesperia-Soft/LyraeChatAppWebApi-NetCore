import axios from 'axios';

const baseURL = "https://localhost:7246"

class GenericApiService {

    async post(path, data) {
        const response = await axios.post(`${baseURL}${path}`, data);
        localStorage.setItem('token', JSON.stringify(response.data));
    }

}

export default GenericApiService;