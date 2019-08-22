FROM microsoft/mssql-server-linux:latest
WORKDIR /opt/var/scripts
COPY ./OBezhan.CustomerService.Database/OBezhan.CustomerService.Database_Create.sql ./
ENV ACCEPT_EULA=Y
CMD (until /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P ${SA_PASSWORD} -i OBezhan.CustomerService.Database_Create.sql; do sleep 5; done; echo 'DB has been published successfully') \
	& /opt/mssql/bin/sqlservr