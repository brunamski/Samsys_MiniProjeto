import axios from "axios";

const baseUrl = "http://localhost:5150/api/"

export default {
    book(url = baseUrl + "Livro/") {
        return {
            fetchAll: () => axios.get(url + "livros/"),
            fetchByIsbn: (isbn) => axios.get(url + 'livros/' + isbn),
            create: newBook => axios.post(url + 'criarLivro/', newBook),
            update: (isbn, updateBook) => axios.patch(url + 'atualizarLivro/' + isbn, updateBook),
            delete: (isbn) => axios.delete(url + 'apagarLivro/' + isbn)
        }
    }
}