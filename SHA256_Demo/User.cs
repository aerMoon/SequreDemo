﻿namespace SHA256_Demo
{
    public class User
    {
        public string Name { get; set; }    
        public string Salt { get; set; }
        public string SaltedHashedPassword { get; set; }
    }
}
