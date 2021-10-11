export default function authHeader(){

    let token = sessionStorage.getItem("accessJWT");
    if (token) {
       return{
           'Content-Type':'application/json',
           'Authorization':token
       }; 
    }else{
        return{};
    }
}