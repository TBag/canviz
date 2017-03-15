module.exports = function (context, data) {

    let tags = data.tags,
        len = tags.length,
        confidence = 0;

    try {
        for (var i = 0; i < len; i++) {
            if (tags[i].name == "indoor") {
                confidence = tags[i].confidence;
            }
            if (tags[i].name == "water") {
                confidence += tags[i].confidence;
            }
        }

        if (confidence > 0.5) {
            // Response of the function to be used later.
            context.res = {
                status: 200,
                body: {
                    'confidence': confidence
                }
            };
        } else {
            // Response of the function to be used later.
            context.res = {
                status: 201,
                body: {
                    'confidence': confidence
                }
            };
        }
    } catch (e) {
        // Response of the function to be used later.
        context.res = {
            status: 400,
            body: e
        };
    };
    context.done();
};