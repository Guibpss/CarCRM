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

// A regra "number" padrao do jQuery Validate so aceita o formato ingles (1,234.56).
// Campos decimais renderizados por asp-for geram data-val-number, entao valores
// mascarados em pt-BR (1.234,56) seriam recusados e o submit era cancelado em silencio.
$.validator.methods.number = function (value, element) {
    return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:\.\d{3})+)(?:,\d+)?$/.test(value);
};