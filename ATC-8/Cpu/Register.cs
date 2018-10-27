namespace ATC8.Cpu
{
    public class Register
    {
        public RegisterName Name { get; }
        public Word Value { get; set; }

        public RegisterAccessRights InternalRights { get; }
        public RegisterAccessRights ExternalRights { get; }

        public Register(RegisterName name, Word value,
            RegisterAccessRights internalRights =
                RegisterAccessRights.Read | RegisterAccessRights.Write,
            RegisterAccessRights externalRights = 
                RegisterAccessRights.Read | RegisterAccessRights.Write)
        {
            Name = name;
            InternalRights = internalRights;
            ExternalRights = externalRights;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}