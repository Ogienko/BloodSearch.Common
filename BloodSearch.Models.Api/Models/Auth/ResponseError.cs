using BloodSearch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BloodSearch.Models.Api.Models.Auth {

    public static class ResponseError {

        private static readonly List<KeyMsg> Error = new List<KeyMsg>() {
          new KeyMsg { Key =  (int)TypeError.UserNotFound, Message = "Не удается войти. Пожалуйста, проверьте правильность написания логина и пароля." },
          new KeyMsg { Key =  (int)TypeError.EmailNotFound, Message = "Такого электронного почтового адреса не существует." },
          new KeyMsg { Key =  (int)TypeError.WhichUserIsAlready,  Message = "Такой пользователь уже есть." },
          new KeyMsg { Key =  (int)TypeError.UserIsNotAuthorized,  Message = "Пользователь не авторизован." },
          new KeyMsg { Key =  (int)TypeError.DataSaveError,  Message = "Ошибка сохранения в базу данных." },
          new KeyMsg { Key =  (int)TypeError.RequestEmpty, Message =  "Пустой запрос." },
        };

        public static KeyMsg GetError(TypeError key) {
            if (Error.All(_ => _.Key != (int)key)) {
                throw new Exception($"Данного ключа:{key} нет в списке.");
            }
            return Error.FirstOrDefault(_ => _.Key == (int)key);
        }
    }

    public enum TypeError {
        UserNotFound,
        EmailNotFound,
        WhichUserIsAlready,
        UserIsNotAuthorized,
        DataSaveError,
        RequestEmpty
    }
}