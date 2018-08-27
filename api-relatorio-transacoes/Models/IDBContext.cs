using System.Collections.Generic;

public enum SearchType { cnpj, brandname, acquirer }
public interface IDBContext<T>
    {
        List<T> GetByType<T>(SearchType type, string elements);
        List<T> GetItem<T>(string filter);
    }