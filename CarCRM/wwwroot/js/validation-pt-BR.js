$.extend($.validator.messages, {
    required: "Este campo é obrigatório.",
    remote: "Corrija este campo.",
    email: "Informe um endereço de e-mail válido.",
    url: "Informe uma URL válida.",
    date: "Informe uma data válida.",
    dateISO: "Informe uma data válida (ISO).",
    number: "Informe um número válido.",
    digits: "Informe apenas números.",
    creditcard: "Informe um cartão de crédito válido.",
    equalTo: "Os valores informados não são iguais.",
    maxlength: $.validator.format("Informe no máximo {0} caracteres."),
    minlength: $.validator.format("Informe pelo menos {0} caracteres."),
    rangelength: $.validator.format("Informe entre {0} e {1} caracteres."),
    range: $.validator.format("Informe um valor entre {0} e {1}."),
    max: $.validator.format("Informe um valor menor ou igual a {0}."),
    min: $.validator.format("Informe um valor maior ou igual a {0}.")
});