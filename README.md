# OilTrendApplication

### Procedura di build e avvio dell'applicazione di OilTrendApplication

**PREMESSA**: affinché la procedura di build possa essere completata con successo è necessario che sulla macchina di sviluppo siano presenti i seguenti requisiti software:

- IDE Visual Studio (vers. 2015/2017/2019/2022) con supporto .NET Framework 4.6.1
- Client per inviare richieste http (es. Postman o Browser web)


L'applicazione OilTrendApplication permette l'interrogazione di un archivio di prezzi Brent EU giornalieri. La lista fa riferimento a https://datahub.io/core/oil-prices/r/brent-daily.json
I dati presenti in archivio sono compresi in un periodo che va dal 20/05/1987 al 28/08/2020.
La tecnologia utilizzata è .NET Framework 4.6.1 in linguaggio C# e implementa il protocollo [JSON-RPC](https://www.jsonrpc.org/specification) con streaming su HTTP.
All'avvio dell'applicazione, il sistema interroga l'archivio dei prezzi e salva tutti i dati in un file SQLite su cui eseguirà tutte le interrogazioni.

Il progetto è una soluzione VS con due progetti principali **OilTrendApplication** e **OilTrendApplicationTest**

### Esecuzione OilTrendApplication

- Dalla Solution Explorer fare un build dell'intera soluzione. 
- Eseguire il progetto OilTrendApplication
- All'avvio viene visualizzato un prompt dei comandi, il messaggio **Listening for JSON-RPC requests** conferma che l'applicazione è attiva e in ascolto
- L'applicazione gira sulla porta 5000 di localhost. Per inviare delle richieste, come da protocollo JSON-RPC,eseguire delle chiamate GET o POST:
- I parametri principali per le richieste sono:
	-	[method] rappresenta il metodo invocato (nel nostro caso oilprice.Trend)
	-	[id] identificativo alfanumerico associato alla chiamata 
	-	[startDateISO8601] data inizio ricerca in formato ISO 'YYYY-MM-DD'
	-	[endDateISO8601] data fine ricerca in formato ISO 'YYYY-MM-DD'
```
I parametri di input [startDateISO8601] ed [endDateISO8601] possono essere valorizzati come segue: 
-[startDateISO8601] ed il [endDateISO8601] valorizzati limita la ricerca entro gli estremi
-[startDateISO8601] non valorizzato ed il [endDateISO8601] valorizzato verranno estratti tutti i record con data uguale/precedente alla data fine
-[startDateISO8601] valorizzato ed il [endDateISO8601] non valorizzato verranno estratti tutti i record con data uguale/successiva alla data di inizio
-[startDateISO8601] ed [endDateISO8601] entrambi non valororizzati, verranno estratti tutte i dati presenti in db
```
### ESEMPIO Chiamata GET

Una volta avviato il servizio, eseguire la chiamata
```
GET http://localhost:5000/?id=1&method=oilprice.Trend&startDateISO8601=2015-01-08&endDateISO8601=2015-02-08
```
Response:
```json
{
    "jsonrpc": "2.0",
    "id": "1",
    "result": {
        "prices": [
            {
                "dateISO8601": "2015-01-08",
                "price": 49.43
            },
            {
                "dateISO8601": "2015-01-09",
                "price": 47.64
            },
            ...
        ]
    },
    "error": null
}
```
### ESEMPIO Chiamata POST
```
POST http://localhost:5000
```
Body
```json
{
	"jsonrpc": "2.0",
	"method": "oilprice.Trend",
	"params": {
		"startDateISO8601": "2015-01-08",
		"endDateISO8601": "2015-02-08"
	},
	"id": "1"
}
```
Response
```json
{
    "jsonrpc": "2.0",
    "id": "1",
    "result": {
        "prices": [
            {
                "dateISO8601": "2015-01-08",
                "price": 49.43
            },
            {
                "dateISO8601": "2015-01-09",
                "price": 47.64
            },
            ...
        ]
    },
    "error": null
}
```
### Esecuzione progetto OilTrendApplicationTest
**PREMESSA** Affinchè l'applicazione funzioni è necessario avviare l'exe dalla cartella di progetto al percorso ..\OilTrendApplication\OilTrendApplication\bin\\[Profilo di avvio]\OilTrendApplication.exe. Nel caso in cui non si sia ancora compilato il progetto procedere con la compilazione e avviare l'exe generato.

### Come avviare il test
Accertarsi che l'exe sia avviato e sia visibile il prompt dei comandi con il messaggio **Listening for JSON-RPC requests**.
Entrare in Visual Studio e dalla Solution Explorer selezionare il progetto **OilTrendApplicationTests**, tasto destro e selezione del comando **Run Tests**.
I risultati saranno disponibili nella sezione Test Explorer.
