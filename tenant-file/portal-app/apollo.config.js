module.exports = {
    client: {
        localSchemaFile: 'graphql-schema.json',
        includes: ['client-schema.graphql', 'graphql-schema.json',
            './src/**'],
        excludes: ['./src/types/**.**'],
        service: {
            url: "http://localhost:8080/graphql",
            skipSSLValidation: true,
        }
    }
};