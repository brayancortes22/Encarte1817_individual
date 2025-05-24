namespace Entity.Enums
{
    /// <summary>
    /// Tipos de contrato disponibles para empleados
    /// </summary>
    public enum ContractType
    {
        /// <summary>
        /// Contrato indefinido/permanente
        /// </summary>
        Indefinido = 1,
        
        /// <summary>
        /// Contrato a término fijo
        /// </summary>
        TerminoFijo = 2,
        
        /// <summary>
        /// Contrato por obra o labor
        /// </summary>
        ObraLabor = 3,
        
        /// <summary>
        /// Contrato de aprendizaje
        /// </summary>
        Aprendizaje = 4,
        
        /// <summary>
        /// Contrato temporal o eventual
        /// </summary>
        Temporal = 5,
        
        /// <summary>
        /// Contrato de prestación de servicios
        /// </summary>
        PrestacionServicios = 6,
        
        /// <summary>
        /// Contrato de prácticas profesionales
        /// </summary>
        Practicas = 7,
        
        /// <summary>
        /// Contrato a medio tiempo
        /// </summary>
        MedioTiempo = 8,
        
        /// <summary>
        /// Contrato por horas
        /// </summary>
        PorHoras = 9,
        
        /// <summary>
        /// Otro tipo de contrato no especificado
        /// </summary>
        Otro = 99
    }
}