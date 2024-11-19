$(function () {
    $("#Amount").maskMoney({ prefix: '$ ', allowNegative: false, thousands: '.', decimal: '.', affixesStay: false });
    $("#Amount").val($("#amt").val());
    var stripe = Stripe($("#StripePublishableKey").val());
    var elements = stripe.elements();
    var style = {
        base: {
            iconColor: '#666EE8',
            color: '#31325F',
            lineHeight: '40px',
            fontWeight: 300,
            fontFamily: 'Helvetica Neue',
            fontSize: '15px',

            '::placeholder': {
                color: '#CFD7E0'
            }
        }
    };

    var cardNumberElement = elements.create('cardNumber', {
        style: style
    });

    cardNumberElement.mount('#card-number-element');

    var cardExpiryElement = elements.create('cardExpiry', {
        style: style
    });

    cardExpiryElement.mount('#card-expiry-element');

    var cardCvcElement = elements.create('cardCvc', {
        style: style
    });

    cardCvcElement.mount('#card-cvc-element');
    cardNumberElement.on('change', function (event) {
        var errorElement = document.querySelector('.error');
        errorElement.classList.remove('visible');
        if (event.error) {
            errorElement.textContent = event.error.message;
            errorElement.classList.add('visible');
            $('#submitButton').prop('disabled', true);
        } else {
            errorElement.textContent = '';
            $('#submitButton').prop('disabled', false);
        }
    });

    cardExpiryElement.on('change', function (event) {
        var errorElement = document.querySelector('.error');
        errorElement.classList.remove('visible');
        if (event.error) {
            errorElement.textContent = event.error.message;
            errorElement.classList.add('visible');
            $('#submitButton').prop('disabled', true);
        } else {
            errorElement.textContent = '';
            $('#submitButton').prop('disabled', false);
        }
    });

    cardCvcElement.on('change', function (event) {
        var errorElement = document.querySelector('.error');
        errorElement.classList.remove('visible');
        if (event.error) {
            errorElement.textContent = event.error.message;
            errorElement.classList.add('visible');
            $('#submitButton').prop('disabled', true);
        } else {
            errorElement.textContent = '';
            $('#submitButton').prop('disabled', false);
        }
    });

    $("#custom-amount").maskMoney({ prefix: '', allowNegative: false, thousands: '.', decimal: '.', affixesStay: false });
    $("#custom-amount").val($("#amt").val());

    $('button[name^="btn-custom-amount"]').on("click", function () {
        var amount = $(this).data("amount");
        if (amount != null) {
            $("#Amount").val(amount);
            $('#custom-amount-group').addClass('hidden');
        }
        else {
            $('#custom-amount-group').removeClass('hidden');
            var customAmount = $('input[id^="custom-amount"]').val();
            $("#Amount").val((customAmount != null) ? customAmount : $("#amt").val());
        }
    });

    $('#custom-amount').on('change keyup paste input', function () {
        var customAmount = $('#custom-amount').val();
        $("#Amount").val((customAmount != null) ? customAmount : $("#amt").val());
    })

    var form = document.getElementById('payment-form');

    form.addEventListener('submit', function (e) {
        e.preventDefault();
        if ($("#Amount").val() < 100) {
            swal({
                text: 'Minimum funding amount is $100.00, Thank you.',
                icon: "info"
            });
            return;
        }
        $('#submitButton').prop('disabled', true);

        var options = {
            address_zip: document.getElementById('postal-code').value,
        };

        stripe.createToken(cardNumberElement, options).then(setOutcome);
    });

    function setOutcome(result) {
        var errorElement = document.querySelector('.error');
        errorElement.classList.remove('visible');

        if (result.token) {
            var form = document.getElementById('payment-form');
            var hiddenInput = document.createElement('input');
            hiddenInput.setAttribute('type', 'hidden');
            hiddenInput.setAttribute('name', 'stripeToken');
            hiddenInput.setAttribute('value', result.token.id);
            form.appendChild(hiddenInput);
            form.submit();
        } else if (result.error) {
            errorElement.textContent = result.error.message;
            errorElement.classList.add('visible');
        }
    }
})