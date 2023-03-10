import api from "./api";

export const ACTION_TYPES = {
    CREATE: 'CREATE',
    UPDATE: 'UPDATE',
    DELETE: 'DELETE',
    FETCH_ALL: 'FETCH_ALL',
}

const formateData = data => ({
    ...data,
    preco: parseFloat(data.preco)
})

export const fetchAll = () => dispatch => {
    api.book().fetchAll()
        .then(response => {
            dispatch({
                type: ACTION_TYPES.FETCH_ALL,
                payload: response.data
            })
        })
        .catch(err => console.log(err))
}

export const create = (data, onSuccess) => dispatch => {
    data = formateData(data)
    api.book().create(data).then(res => {
        dispatch({
            type: ACTION_TYPES.CREATE,
            payload: res.data
        })
        onSuccess()
    })
    .catch(err => console.log(err))
}

export const update = (isbn, data, onSuccess) => dispatch => {
    data = formateData(data)
    api.dCandidate().update(isbn, data)
        .then(res => {
            dispatch({
                type: ACTION_TYPES.UPDATE,
                payload: { isbn, ...data }
            })
            onSuccess()
        })
        .catch(err => console.log(err))
}

export const Delete = (isbn, onSuccess) => dispatch => {
    api.dCandidate().delete(isbn)
        .then(res => {
            dispatch({
                type: ACTION_TYPES.DELETE,
                payload: isbn
            })
            onSuccess()
        })
        .catch(err => console.log(err))
}