import { ACTION_TYPES } from "../actions/book";

const initialState = {
    list: []
}

export const book = (state = initialState, action) => {

    switch (action.type) {
        case ACTION_TYPES.FETCH_ALL:
            return {
                ...state,
                list: [...action.payload]
            }

        case ACTION_TYPES.FETCH_ISBN:
            return {
                ...state,
                list: state.list.map(x => x.isbn === action.payload.isbn ? action.payload : x)
        }

        case ACTION_TYPES.CREATE:
            return {
                 ...state,
                list: [...state.list, action.payload]
            }
    
        case ACTION_TYPES.UPDATE:
            return {
                ...state,
                list: state.list.map(x => x.isbn === action.payload.isbn ? action.payload : x)
            }
    
        case ACTION_TYPES.DELETE:
            return {
                ...state,
                list: state.list.filter(x => x.isbn !== action.payload)
            }

        default:
            return state
    }
}