using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CoreConsole.WS;

namespace CoreConsole
{
    class Program
    {
        #region log y contraseña
        public static string EnteredVal = "";
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion
        static void Main(string[] args)
        {
            #region variables y soapClient
            string answer, Consoleaccount, salir = "No";
            int intento = 1, intentorestante = 2;

            Console.Title = "CoreServer";

            //Connetion
            CoreWSSoapClient soapClient = new CoreWSSoapClient();
            #endregion

            while (intento < 4)
            {
                #region log in
                //Log in 
                Console.WriteLine("Inserte su usuario:");
                Consoleaccount = Console.ReadLine();
                string enterText = "Inserte su contraseña: ";
                CheckPassword(enterText);
                var Consoleuser = soapClient.ValidateEmployeeUserCredentials(Consoleaccount, EnteredVal);
                #endregion

                if (Consoleuser != null)
                {
                    #region premenu
                    log.Info($"El usuario {Consoleaccount} se logeó");
                    Console.Clear();
                    //MENU
                    Console.WriteLine("Bienvenido al menú del sistema principal!");
                    Console.WriteLine("Opciones:");
                    #endregion
                    while (salir == "No")
                    {
                        #region MENU PRINCIPAL
                        Console.WriteLine("1 para opciones de usuario");
                        Console.WriteLine("2 para opciones de cliente");
                        Console.WriteLine("3 para opciones de cuentas");
                        Console.WriteLine("4 para opciones de empleado!");
                        Console.WriteLine("5 para opciones de transaccion");
                        Console.WriteLine("6 para cerrar el sistema");
                        answer = Console.ReadLine();
                        Console.WriteLine("Loading...");
                        Thread.Sleep(2000);
                        Console.Clear();
                        #endregion
                        switch (answer)
                        {
                            #region MENU USUARIO
                            //MENU USUARIO
                            case "1":
                                string respu;
                                Console.WriteLine("Bienvenido al menú de usuarios!");
                                Console.WriteLine("Opciones:");
                                Console.WriteLine("1 para crear usuario");
                                Console.WriteLine("2 para actualizar usuario");
                                Console.WriteLine("3 para eliminar un usuario");
                                Console.WriteLine("4 para buscar un usuario por su ID");
                                Console.WriteLine("5 para buscar el listado de usuarios");
                                Console.WriteLine("6 para salir de este menú");
                                respu = Console.ReadLine();
                                Console.WriteLine("Loading...");
                                Thread.Sleep(2000);
                                Console.Clear();
                                switch (respu)
                                {
                                    //Insertar usuario
                                    case "1":
                                        string user1, password1, email1;
                                        int id1;
                                        Console.WriteLine("Inserte su usuario");
                                        user1 = Console.ReadLine();
                                        Console.WriteLine("Inserte su correo");
                                        email1 = Console.ReadLine();
                                        Console.WriteLine("Inserte su contraseña");
                                        password1 = Console.ReadLine();
                                        Console.WriteLine("Inserte su ID");
                                        id1 = int.Parse(Console.ReadLine());
                                        User UserToCreate = new User
                                        {
                                            UserName = user1,
                                            Email = email1,
                                            Password = password1,
                                            ClientId = id1
                                        };
                                        Console.WriteLine(soapClient.InsertUser(UserToCreate));
                                        log.Info($"Inserción del usuario {UserToCreate.UserName}, por el empleado{Consoleaccount}");
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        break;

                                    //Actualizar usuario
                                    case "2":
                                        string user2, password2, email2;
                                        int id2;
                                        Console.WriteLine("Inserte su usuario");
                                        user2 = Console.ReadLine();
                                        Console.WriteLine("Inserte su correo");
                                        email2 = Console.ReadLine();
                                        Console.WriteLine("Inserte su contraseña");
                                        password2 = Console.ReadLine();
                                        Console.WriteLine("Inserte su ID");
                                        id2 = int.Parse(Console.ReadLine());
                                        User UserToUpdate = new User
                                        {
                                            UserName = user2,
                                            Email = email2,
                                            Password = password2,
                                            ClientId = id2
                                        };
                                        Console.WriteLine(soapClient.UpdateUser(UserToUpdate));
                                        log.Info($"Actualizacion del usuario {UserToUpdate.UserName}, por el empleado{Consoleaccount}");
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        break;

                                    //ELIMINAR USUARIO
                                    case "3":
                                        int CLientID;
                                        string confirmacion;
                                        Console.WriteLine("Inserte el ID del cliente");
                                        CLientID = int.Parse(Console.ReadLine());

                                        User UserToEliminate = soapClient.GetUserByClientId(CLientID);
                                        Console.WriteLine($"ID: {UserToEliminate.Id}\nEmail: {UserToEliminate.Email}\nUsername: {UserToEliminate.UserName}");
                                        Console.WriteLine("Desea eliminarlo? {s/n}");
                                        confirmacion = Console.ReadLine();
                                        if (confirmacion == "s")
                                        {
                                            Console.WriteLine(soapClient.DeleteUser(UserToEliminate.Id));
                                            log.Info($"Elminacion del usuario {UserToEliminate.UserName}, por el empleado{Consoleaccount}");
                                        }

                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        break;

                                    //Buscar un usuario por su ID
                                    case "4":
                                        int CLientID2;
                                        Console.WriteLine("Inserte el ID del cliente");
                                        CLientID2 = int.Parse(Console.ReadLine());

                                        User UserToSearch = soapClient.GetUserByClientId(CLientID2);
                                        Console.WriteLine($"ID: {UserToSearch.Id}\nEmail: {UserToSearch.Email}\nUsername: {UserToSearch.UserName}");
                                        log.Info($"Se buscó el usuario {UserToSearch.UserName}, por el empleado{Consoleaccount}");
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        break;

                                    //Buscar lista de usuarios
                                    case "5":
                                        List<User> listado = new List<User>(soapClient.GetAllUsers());
                                        foreach (User user in listado)
                                        {
                                            Console.WriteLine($"Usuario: {user.UserName}, correo: {user.Email}, Id del cliente: {user.ClientId}");
                                        };
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        Console.Clear();
                                        break;

                                    //Opción incorrecta o salir
                                    default:
                                        Console.WriteLine("Saliendo de menú...");
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        break;
                                }
                                break;
                            #endregion
                            #region MENU CLIENTE
                            //Menu Clientes
                            case "2":
                                string respuesta;
                                Console.WriteLine("Bienvenido al menú de cliente");
                                Console.WriteLine("Opciones:");
                                Console.WriteLine("1 para insertar un cliente");
                                Console.WriteLine("2 para actualizar un cliente");
                                Console.WriteLine("3 para eliminar un cliente");
                                Console.WriteLine("4 para buscar todos los clientes");
                                Console.WriteLine("5 para buscar cliente por su id");
                                Console.WriteLine("6 para salir de este menú");
                                respuesta = Console.ReadLine();
                                Console.WriteLine("Loading...");
                                Thread.Sleep(2000);
                                Console.Clear();
                                switch (respuesta)
                                {
                                    //Insertar cliente
                                    case "1":
                                        string name, lastname, identification, adress, teleph;
                                        int genderid, identificationid;
                                        Console.WriteLine("Inserte su nombre");
                                        name = Console.ReadLine();
                                        Console.WriteLine("Inserte su apellido");
                                        lastname = Console.ReadLine();
                                        Console.WriteLine("Que tipo de identificacion? 0 para cedula, 1 para pasaporte");
                                        identificationid = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Inserte la identificacion");
                                        identification = Console.ReadLine();
                                        Console.WriteLine("Inserte su telefono");
                                        teleph = Console.ReadLine();
                                        Console.WriteLine("Inserte su dirección");
                                        adress = Console.ReadLine();
                                        Console.WriteLine("Inserte su genero: 0 para masculino, 1 para femenino");
                                        genderid = int.Parse(Console.ReadLine());
                                        Client cliente = new Client
                                        {
                                            Address = adress,
                                            Telephone = teleph,
                                            Id = 0,
                                            Identification = identification,
                                            IdentificationType = (IdentificationType)identificationid,
                                            Gender = (Gender)genderid,
                                            Name = name,
                                            LastName = lastname

                                        };
                                        Console.WriteLine($"Tipo: {cliente.IdentificationType.ToString()}");
                                        Console.ReadLine();
                                        Console.WriteLine(soapClient.InsertClient(cliente));
                                        log.Info($"Insercion del cliente {cliente.Name}, apellido: {cliente.LastName} por el empleado{Consoleaccount}");
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        break;
                                    //Actualizar cliente
                                    case "2":
                                        string name2, lastname2, identification2, adress2, teleph2;
                                        int genderid2, identificationid2;
                                        Console.WriteLine("Inserte su nombre");
                                        name2 = Console.ReadLine();
                                        Console.WriteLine("Inserte su apellido");
                                        lastname2 = Console.ReadLine();
                                        Console.WriteLine("Que tipo de identificacion? 0 para cedula, 1 para pasaporte");
                                        identificationid2 = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Inserte la identificacion");
                                        identification2 = Console.ReadLine();
                                        Console.WriteLine("Inserte su telefono");
                                        teleph2 = Console.ReadLine();
                                        Console.WriteLine("Inserte su dirección");
                                        adress2 = Console.ReadLine();
                                        Console.WriteLine("Inserte su genero: 0 para masculino, 1 para femenino");
                                        genderid2 = int.Parse(Console.ReadLine());
                                        Client clienteToUpdate = new Client
                                        {
                                            Address = adress2,
                                            Telephone = teleph2,
                                            Id = 0,
                                            Identification = identification2,
                                            IdentificationType = (IdentificationType)identificationid2,
                                            Gender = (Gender)genderid2,
                                            Name = name2,
                                            LastName = lastname2

                                        };
                                        Console.WriteLine(soapClient.UpdateClient(clienteToUpdate));
                                        log.Info($"Actualizacion del cliente {clienteToUpdate.Name}, apellido: {clienteToUpdate.LastName} por el empleado{Consoleaccount}");
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        break;

                                    //Eliminar Cliente
                                    case "3":
                                        string CLientID;
                                        string confirmacion;
                                        Console.WriteLine("Inserte la identificacion del cliente");
                                        CLientID = (Console.ReadLine());


                                        Client ClientToEliminate = soapClient.GetClientByIdentification(CLientID);
                                        Console.WriteLine($"ID: {ClientToEliminate.Id}\nIdentificacion: {ClientToEliminate.Identification}\nName: {ClientToEliminate.Name}");
                                        Console.WriteLine("Desea eliminarlo? {s/n}");
                                        confirmacion = Console.ReadLine();
                                        if (confirmacion == "s")
                                        {
                                            Console.WriteLine(soapClient.DeleteClient(ClientToEliminate.Id));
                                            log.Info($"Eliminacion del cliente {ClientToEliminate.Name}, apellido: {ClientToEliminate.LastName} por el empleado{Consoleaccount}");
                                        }
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        break;

                                    //Buscar Listado completo
                                    case "4":

                                        var listadoClientes = soapClient.GetAllClients();
                                        foreach (Client client in listadoClientes)
                                        {
                                            Console.WriteLine($"Nombre: {client.Name}, Apellido: {client.LastName}, telefono: {client.Telephone}, direccion: {client.Address}, identificacion: {client.Identification}");

                                        };
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        Console.Clear();
                                        Console.WriteLine();
                                        break;

                                    //Buscar por su id
                                    case "5":
                                        string id3;
                                        Console.WriteLine("Inserte su identificacion");
                                        id3 = (Console.ReadLine());

                                        Client clienteBuscado = soapClient.GetClientByIdentification(id3);
                                        Console.WriteLine($"Nombre: {clienteBuscado.Name}, apellido: {clienteBuscado.LastName}, telefono: {clienteBuscado.Telephone}, direccion: {clienteBuscado.Address}, identificacion: {clienteBuscado.Identification}");
                                        log.Info($"Se buscó el cliente {clienteBuscado.Name}, apellido: {clienteBuscado.LastName} por el empleado{Consoleaccount}");
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        break;

                                    //Opcion invalida o Salir de menu
                                    default:
                                        Console.WriteLine("Saliendo de menú...");
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        break;
                                }
                                break;
                            #endregion
                            #region MENU CUENTAS
                            case "3":
                                //Menu principal
                                string resp;
                                Console.WriteLine("Bienvneido al Menú de cuentas");
                                Console.WriteLine("Opciones:");
                                Console.WriteLine("1 para crear una cuenta");
                                Console.WriteLine("2 para editar una cuenta");
                                Console.WriteLine("3 para borrar una cuenta");
                                Console.WriteLine("4 para buscar el listado de cuentas");
                                Console.WriteLine("5 para buscar cuenta por el ID");
                                Console.WriteLine("6 para salir de este menú");
                                resp = Console.ReadLine();
                                Console.WriteLine("Loading...");
                                Thread.Sleep(2000);
                                Console.Clear();

                                switch (resp)
                                {
                                    case "1":
                                        string alias, numero, identifi;
                                        int Ownerid, accountType, CurrencyType;
                                        Console.WriteLine("Inserte el numero de cuenta");
                                        numero = Console.ReadLine();
                                        Console.WriteLine("Inserte su alias");
                                        alias = Console.ReadLine();
                                        Console.WriteLine("Inserte el tipo de cuenta {1 para saving, 2 para current}");
                                        accountType = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Inserte el tipo de dinero");
                                        CurrencyType = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Inserte su identificacion");
                                        identifi = Console.ReadLine();
                                        Client Cliente = soapClient.GetClientByIdentification(identifi);
                                        Ownerid = Cliente.Id;


                                        Account cuenta = new Account
                                        {
                                            
                                            Alias = alias,
                                            AccountType = (AccountType)accountType,
                                            CurrencyType = (WS.CurrencyType)CurrencyType,
                                            AccountManagerId = 1,
                                            OwnerId = Ownerid
                                        };
                                        Console.WriteLine(soapClient.InsertAccount(cuenta));
                                        Console.ReadLine();
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            #endregion
                            #region MENU Empleados
                            case "4":
                                string respuemp;
                                Console.WriteLine("Bienvenido al menú de usuarios empleados!");
                                Console.WriteLine("Opciones:");
                                Console.WriteLine("1 para crear usuario");
                                Console.WriteLine("2 para actualizar usuario");
                                Console.WriteLine("3 para eliminar un usuario");
                                Console.WriteLine("4 para buscar un usuario por su ID");
                                Console.WriteLine("5 para buscar el listado de usuarios");
                                Console.WriteLine("6 para salir de este menú");
                                respuemp = Console.ReadLine();
                                Console.WriteLine("Loading...");
                                Thread.Sleep(2000);
                                Console.Clear(); ;

                                //Opciones
                                switch (respuemp)
                                {

                                    //Crear empleado
                                    case "1":
                                        string empuser1, emppassword1, empemail1;
                                        int rolid1, empid1;
                                        Console.WriteLine("Inserte su usuario");
                                        empuser1 = Console.ReadLine();
                                        Console.WriteLine("Inserte su correo");
                                        empemail1 = Console.ReadLine();
                                        Console.WriteLine("Inserte su contraseña");
                                        emppassword1 = Console.ReadLine();
                                        Console.WriteLine("Inserte su rol ID {0 para admin, 1 para CRUD, 2 para viewer}");
                                        rolid1 = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Inserte el id del empleado");
                                        empid1 = int.Parse(Console.ReadLine());
                                        EmployeeUser EmployeeToCreate = new EmployeeUser
                                        {
                                            UserName = empuser1,
                                            Email = empemail1,
                                            Password = emppassword1,
                                            Role = (Role)rolid1,
                                            EmployeeId = empid1
                                        };
                                        Console.WriteLine(soapClient.InsertEmployeeUser(EmployeeToCreate));
                                        log.Info($"Insercion del empleado {EmployeeToCreate.UserName} por el empleado {Consoleaccount}");
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        break;

                                    //Actualizar empleado
                                    case "2":
                                        string empuser2, emppassword2, empemail2;
                                        int empid2, rolid2;
                                        Console.WriteLine("Inserte su usuario");
                                        empuser2 = Console.ReadLine();
                                        Console.WriteLine("Inserte su correo");
                                        empemail2 = Console.ReadLine();
                                        Console.WriteLine("Inserte su contraseña");
                                        emppassword2 = Console.ReadLine();
                                        Console.WriteLine("Inserte su ID");
                                        empid2 = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Inserte su rol ID {1 para admin, 2 para CRUD, 3 para viewer}");
                                        rolid2 = int.Parse(Console.ReadLine());
                                        EmployeeUser EmployeeToUpdate = new EmployeeUser
                                        {
                                            UserName = empuser2,
                                            Email = empemail2,
                                            Password = emppassword2,
                                            EmployeeId = empid2,
                                            Role = (Role)rolid2

                                        };
                                        Console.WriteLine(soapClient.UpdateEmployeeUser(EmployeeToUpdate));
                                        log.Info($"Actualizacion del empleado {EmployeeToUpdate.UserName} por el empleado {Consoleaccount}");
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        break;

                                    //Delete Employee
                                    case "3":
                                        int EmployeeID;
                                        string confirmacion;
                                        Console.WriteLine("Inserte el ID del empleado");
                                        EmployeeID = int.Parse(Console.ReadLine());

                                        EmployeeUser EmployeeToEliminate = soapClient.GetEmployeeUserByEmployeeId(EmployeeID);
                                        Console.WriteLine($"ID: {EmployeeToEliminate.Id}\nEmail: {EmployeeToEliminate.Email}\nUsername: {EmployeeToEliminate.UserName}");
                                        Console.WriteLine("Desea eliminarlo? {s/n}");
                                        confirmacion = Console.ReadLine();
                                        if (confirmacion == "s")
                                        {
                                            Console.WriteLine(soapClient.DeleteEmployeeUser(EmployeeToEliminate.Id));
                                            log.Info($"Eliminacion del empleado {EmployeeToEliminate.UserName} por el empleado {Consoleaccount}");
                                        }
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        break;

                                    //Buscar empleado por su id
                                    case "4":
                                        int EmployeeID2;
                                        Console.WriteLine("Inserte el ID del empleado");
                                        EmployeeID2 = int.Parse(Console.ReadLine());

                                        EmployeeUser EmployeeToSearch = soapClient.GetEmployeeUserByEmployeeId(EmployeeID2);
                                        Console.WriteLine($"ID: {EmployeeToSearch.Id}\nEmail: {EmployeeToSearch.Email}\nUsername: {EmployeeToSearch.UserName}");
                                        log.Info($"Se buscó el empleado {EmployeeToSearch.UserName} por el empleado {Consoleaccount}");
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        break;

                                    //Buscar lista de empleados
                                    case "5":
                                        List<EmployeeUser> listadoEmpleados = new List<EmployeeUser>(soapClient.GetAllEmployeeUsers());
                                        foreach (EmployeeUser Employeeuser in listadoEmpleados)
                                        {
                                            Console.WriteLine($"Usuario: {Employeeuser.UserName}, correo: {Employeeuser.Email}, Id del empleado: {Employeeuser.EmployeeId}");
                                        };
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        Console.Clear();
                                        break;

                                    //Opcion incorrecta o salir de menu  
                                    default:
                                        Console.WriteLine("Saliendo de menú...");
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        break;
                                }
                                break;
                            #endregion
                            #region MENU TRANSACCIONES
                            case "5":
                                string answer0;
                                Console.WriteLine("Bienvenido al menú de transacciones!");
                                Console.WriteLine("Opciones:");
                                Console.WriteLine("1 para buscar listado de transacciones");
                                Console.WriteLine("2 para buscar por número de transacción");
                                Console.WriteLine("3 para buscar por número de referencia");
                                Console.WriteLine("4 para buscar por Id del ordenante");
                                Console.WriteLine("5 para buscar por Id del beneficiario");
                                Console.WriteLine("6 para salir de este menú");
                                answer0 = Console.ReadLine();
                                Console.WriteLine("Loading...");
                                Thread.Sleep(2000);
                                Console.Clear();
                                switch (answer0)
                                {
                                    //BUSCAR LISTADO
                                    case "1":

                                        List<Transaction> transactions = new List<Transaction>(soapClient.GetAllTransactions());
                                        foreach (Transaction transaccion in transactions)
                                        {
                                            Console.WriteLine($"Cuenta del ordenante: {transaccion.PayerAccount}, identificación del ordenante: {transaccion.PayerIdentification}, Identificacion del beneficiario: {transaccion.PayeeIdentification}, Concepto: {transaccion.Concept}, Monto: {transaccion.Amount}, Numero de transaccion: {transaccion.Number}");
                                        }
                                        log.Info($"El empleado {Consoleaccount} buscó el listado de transacciones");
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        Console.Clear();
                                        break;

                                    //Buscar por número de transaccion
                                    case "2":
                                        string Number;
                                        Console.WriteLine("Inserte el número de transacción");
                                        Number = Console.ReadLine();
                                        Transaction transaction = soapClient.GetTransactionByNumber(Number);
                                        Console.WriteLine($"Cuenta del ordenante: {transaction.PayerAccount}, identificación del ordenante: {transaction.PayerIdentification}, Identificacion del beneficiario: {transaction.PayeeIdentification}, Concepto: {transaction.Concept}, Monto: {transaction.Amount}, Numero de transaccion: {transaction.Number}");
                                        log.Info($"El empleado {Consoleaccount} buscó la transacción {Number}");
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        Console.Clear();
                                        break;

                                    //Buscar por número de referencia
                                    case "3":
                                        string ReferenceNumber;
                                        Console.WriteLine("Inserte el número de referencia");
                                        ReferenceNumber = Console.ReadLine();
                                        Transaction TransactionByReference = soapClient.GetTransactionByReference(ReferenceNumber);
                                        Console.WriteLine($"Cuenta del ordenante: {TransactionByReference.PayerAccount}, identificación del ordenante: {TransactionByReference.PayerIdentification}, Identificacion del beneficiario: {TransactionByReference.PayeeIdentification}, Concepto: {TransactionByReference.Concept}, Monto: {TransactionByReference.Amount}, Numero de transaccion: {TransactionByReference.Number}");
                                        log.Info($"El empleado {Consoleaccount} buscó la transacción con referencia {ReferenceNumber}");
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        Console.Clear();
                                        break;

                                    //Buscar por ID del ordenante
                                    case "4":
                                        string identifi;
                                        Console.WriteLine("Inserte la identificación del ordenante");
                                        identifi = Console.ReadLine();
                                        Client cliente = soapClient.GetClientByIdentification(identifi);
                                        List<Transaction> transacs1 = new List<Transaction>(soapClient.GetTransactionByPayee(cliente.Id));
                                        foreach (Transaction item in transacs1)
                                        {
                                            Console.WriteLine($"Cuenta del ordenante: {item.PayerAccount}, identificación del ordenante: {item.PayerIdentification}, Identificacion del beneficiario: {item.PayeeIdentification}, Concepto: {item.Concept}, Monto: {item.Amount}, Numero de transaccion: {item.Number}");
                                        };
                                        log.Info($"El empleado {Consoleaccount} buscó las transacciones del cliente {identifi}");
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        Console.Clear();
                                        break;

                                    //Buscar por ID del beneficiario
                                    case "5":
                                        string identifi2;
                                        Console.WriteLine("Inserte la identificación del beneficiario");
                                        identifi2 = Console.ReadLine();
                                        Client cliente2 = soapClient.GetClientByIdentification(identifi2);

                                        List<Transaction> transacs2 = new List<Transaction>(soapClient.GetTransactionByPayee(cliente2.Id));
                                        foreach (Transaction item in transacs2)
                                        {
                                            Console.WriteLine($"Cuenta del ordenante: {item.PayerAccount}, identificación del ordenante: {item.PayerIdentification}, Identificacion del beneficiario: {item.PayeeIdentification}, Concepto: {item.Concept}, Monto: {item.Amount}, Numero de transaccion: {item.Number}");
                                        };
                                        
                                        log.Info($"El empleado {Consoleaccount} buscó las transacciones del cliente {identifi2}");
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        Console.Clear();
                                        break;

                                    //Opcion incorrecta o salir
                                    default:
                                        Console.WriteLine("Saliendo de menú...");
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        break;
                                }
                                break;
                            #endregion
                            #region CIERRE DE SISTEMA
                            case "6":
                                Environment.Exit(0);
                                break;
                            #endregion
                            #region OPCION INVALIDA
                            default:
                                Console.WriteLine("Opcion invalida!!");
                                Console.WriteLine("Intente de nuevo!");
                                Thread.Sleep(2000);
                                Console.Clear();
                                break;
                                #endregion
                        }
                    }
                }
                #region Intentos fallidos
                else
                {
                    Console.WriteLine("Intento no valido!");
                    Console.WriteLine($"Intente de nuevo, le quedan {intentorestante} intentos");
                    intento++;
                    intentorestante--;
                    log.Info($"El usuario {Consoleaccount} se intentó logear!");

                }
                #endregion
            }
        }
        #region Metodo para tagear y controlar contraseña
        static void CheckPassword(string EnterText)
        {
            try
            {
                Console.Write(EnterText);
                EnteredVal = "";
                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    // Backspace Should Not Work  
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        EnteredVal += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && EnteredVal.Length > 0)
                        {
                            EnteredVal = EnteredVal.Substring(0, (EnteredVal.Length - 1));
                            Console.Write("\b \b");
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            if (string.IsNullOrWhiteSpace(EnteredVal))
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Empty value not allowed.");
                                CheckPassword(EnterText);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("");
                                break;
                            }
                        }
                    }
                } while (true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        #endregion
    }
}
