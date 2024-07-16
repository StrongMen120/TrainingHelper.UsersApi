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
  AddPermissionCommandDto,
  PermissionDto,
  ProblemDetails,
} from '../models';
import {
    AddPermissionCommandDtoFromJSON,
    AddPermissionCommandDtoToJSON,
    PermissionDtoFromJSON,
    PermissionDtoToJSON,
    ProblemDetailsFromJSON,
    ProblemDetailsToJSON,
} from '../models';

export interface CreatePermissionRequest {
    addPermissionCommandDto?: AddPermissionCommandDto;
}

export interface DeletePermissionRequest {
    permissionId: string;
}

/**
 * PermissionsApi - interface
 * 
 * @export
 * @interface PermissionsApiInterface
 */
export interface PermissionsApiInterface {
    /**
     * 
     * @param {AddPermissionCommandDto} [addPermissionCommandDto] 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof PermissionsApiInterface
     */
    createPermissionRaw(requestParameters: CreatePermissionRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<PermissionDto>>;

    /**
     */
    createPermission(addPermissionCommandDto?: AddPermissionCommandDto, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<PermissionDto>;

    /**
     * 
     * @param {string} permissionId 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof PermissionsApiInterface
     */
    deletePermissionRaw(requestParameters: DeletePermissionRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<PermissionDto>>;

    /**
     */
    deletePermission(permissionId: string, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<PermissionDto>;

}

/**
 * 
 */
export class PermissionsApi extends runtime.BaseAPI implements PermissionsApiInterface {

    /**
     */
    async createPermissionRaw(requestParameters: CreatePermissionRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<PermissionDto>> {
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
            path: `/api/v1/permissions/permission`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: AddPermissionCommandDtoToJSON(requestParameters.addPermissionCommandDto),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => PermissionDtoFromJSON(jsonValue));
    }

    /**
     */
    async createPermission(addPermissionCommandDto?: AddPermissionCommandDto, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<PermissionDto> {
        const response = await this.createPermissionRaw({ addPermissionCommandDto: addPermissionCommandDto }, initOverrides);
        return await response.value();
    }

    /**
     */
    async deletePermissionRaw(requestParameters: DeletePermissionRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<PermissionDto>> {
        if (requestParameters.permissionId === null || requestParameters.permissionId === undefined) {
            throw new runtime.RequiredError('permissionId','Required parameter requestParameters.permissionId was null or undefined when calling deletePermission.');
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
            path: `/api/v1/permissions/permission/{permissionId}`.replace(`{${"permissionId"}}`, encodeURIComponent(String(requestParameters.permissionId))),
            method: 'DELETE',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => PermissionDtoFromJSON(jsonValue));
    }

    /**
     */
    async deletePermission(permissionId: string, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<PermissionDto> {
        const response = await this.deletePermissionRaw({ permissionId: permissionId }, initOverrides);
        return await response.value();
    }

}
