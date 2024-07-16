/* tslint:disable */
/* eslint-disable */
/**
 * Users API
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: all
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


import * as runtime from '../runtime';
import type {
  AddRoleCommandDto,
  ProblemDetails,
  RoleDto,
} from '../models';
import {
    AddRoleCommandDtoFromJSON,
    AddRoleCommandDtoToJSON,
    ProblemDetailsFromJSON,
    ProblemDetailsToJSON,
    RoleDtoFromJSON,
    RoleDtoToJSON,
} from '../models';

export interface CreateRoleRequest {
    addRoleCommandDto: AddRoleCommandDto;
}

export interface DeleteRoleRequest {
    roleId: number;
}

export interface UpdateRoleRequest {
    roleDto: RoleDto;
}

/**
 * RolesApi - interface
 * 
 * @export
 * @interface RolesApiInterface
 */
export interface RolesApiInterface {
    /**
     * 
     * @param {AddRoleCommandDto} addRoleCommandDto 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof RolesApiInterface
     */
    createRoleRaw(requestParameters: CreateRoleRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RoleDto>>;

    /**
     */
    createRole(addRoleCommandDto: AddRoleCommandDto, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RoleDto>;

    /**
     * 
     * @param {number} roleId 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof RolesApiInterface
     */
    deleteRoleRaw(requestParameters: DeleteRoleRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RoleDto>>;

    /**
     */
    deleteRole(roleId: number, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RoleDto>;

    /**
     * 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof RolesApiInterface
     */
    getAllRolesRaw(initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<Array<RoleDto>>>;

    /**
     */
    getAllRoles(initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<Array<RoleDto>>;

    /**
     * 
     * @param {RoleDto} roleDto 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof RolesApiInterface
     */
    updateRoleRaw(requestParameters: UpdateRoleRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RoleDto>>;

    /**
     */
    updateRole(roleDto: RoleDto, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RoleDto>;

}

/**
 * 
 */
export class RolesApi extends runtime.BaseAPI implements RolesApiInterface {

    /**
     */
    async createRoleRaw(requestParameters: CreateRoleRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RoleDto>> {
        if (requestParameters.addRoleCommandDto === null || requestParameters.addRoleCommandDto === undefined) {
            throw new runtime.RequiredError('addRoleCommandDto','Required parameter requestParameters.addRoleCommandDto was null or undefined when calling createRole.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("BearerJWT", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/api/v1/roles/role`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: AddRoleCommandDtoToJSON(requestParameters.addRoleCommandDto),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => RoleDtoFromJSON(jsonValue));
    }

    /**
     */
    async createRole(addRoleCommandDto: AddRoleCommandDto, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RoleDto> {
        const response = await this.createRoleRaw({ addRoleCommandDto: addRoleCommandDto }, initOverrides);
        return await response.value();
    }

    /**
     */
    async deleteRoleRaw(requestParameters: DeleteRoleRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RoleDto>> {
        if (requestParameters.roleId === null || requestParameters.roleId === undefined) {
            throw new runtime.RequiredError('roleId','Required parameter requestParameters.roleId was null or undefined when calling deleteRole.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("BearerJWT", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/api/v1/roles/role/{roleId}`.replace(`{${"roleId"}}`, encodeURIComponent(String(requestParameters.roleId))),
            method: 'DELETE',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => RoleDtoFromJSON(jsonValue));
    }

    /**
     */
    async deleteRole(roleId: number, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RoleDto> {
        const response = await this.deleteRoleRaw({ roleId: roleId }, initOverrides);
        return await response.value();
    }

    /**
     */
    async getAllRolesRaw(initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<Array<RoleDto>>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("BearerJWT", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/api/v1/roles/roles`,
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => jsonValue.map(RoleDtoFromJSON));
    }

    /**
     */
    async getAllRoles(initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<Array<RoleDto>> {
        const response = await this.getAllRolesRaw(initOverrides);
        return await response.value();
    }

    /**
     */
    async updateRoleRaw(requestParameters: UpdateRoleRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RoleDto>> {
        if (requestParameters.roleDto === null || requestParameters.roleDto === undefined) {
            throw new runtime.RequiredError('roleDto','Required parameter requestParameters.roleDto was null or undefined when calling updateRole.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("BearerJWT", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/api/v1/roles/role`,
            method: 'PUT',
            headers: headerParameters,
            query: queryParameters,
            body: RoleDtoToJSON(requestParameters.roleDto),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => RoleDtoFromJSON(jsonValue));
    }

    /**
     */
    async updateRole(roleDto: RoleDto, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RoleDto> {
        const response = await this.updateRoleRaw({ roleDto: roleDto }, initOverrides);
        return await response.value();
    }

}
